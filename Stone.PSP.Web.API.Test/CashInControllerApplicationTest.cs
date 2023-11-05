using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Enumerators;
using Stone.PSP.Domain.Repositories;
using Stone.PSP.Domain.Services;
using Stone.PSP.Domain.UnitOfWork;
using Stone.PSP.Infra.Context;
using Stone.PSP.Infra.UnitOfWork;
using Stone.PSP.TransactionService.Automapper;
using Stone.PSP.Web.API.Controllers;
using TransactionService.Services;
using TransactionService.ViewModels;

namespace Stone.PSP.Web.API.Test
{
    public class CashInControllerApplicationTest
    {
        private readonly Mock<PaymentContext> _context = new Mock<PaymentContext>();
        private readonly Mock<IPspTransactionRepository> _pspTransactionRepository = new Mock<IPspTransactionRepository>();
        private readonly Mock<IPayableRepository> _payableRepository = new Mock<IPayableRepository>();
        private IFeeConfigurationCacheService _feeConfigurationCacheService = new FeeConfigurationCacheService();
        private readonly UnitOfWork _unitOfWork;
        private readonly Mock<IValidator<PspTransaction>> _transactionValidator = new Mock<IValidator<PspTransaction>>();
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<CashInService>> _logger = new Mock<ILogger<CashInService>>();
        private readonly CashInController _controller;
        private readonly CashInService _cashInService;
        private readonly Mock<IDatabaseTransaction> _dataBaseTransaction = new Mock<IDatabaseTransaction>();
        private Guid ClientId = Guid.NewGuid();
        private TransactionViewModel _transaction = new()
        {
            Value = 500.0M,
            CreditCard = new CreditCardViewModel
            {
                ExpirationDate = DateTime.Today.AddYears(5),
                Holder = "Nome do portador do cartão",
                VerificationCode = "724",
                Number = "5452 4631 0836 7683"
            },
            Client = new ClientViewModel { Id = Guid.NewGuid() },
            Description = "Smartband XYZ 3.0",
            PaymentMethodCode = (byte)PaymentMethodCodeType.Credit
        };

        private TransactionViewModel _expectedTransaction = new()
        {
            Value = 500.0M,
            CreditCard = new CreditCardViewModel
            {
                ExpirationDate = DateTime.Today.AddYears(5),
                Holder = "Nome do portador do cartão",
                VerificationCode = "724",
                Number = "7683"
            },
            Client = new ClientViewModel { Id = Guid.NewGuid() },
            Description = "Smartband XYZ 3.0",
            PaymentMethodCode = (byte)PaymentMethodCodeType.Credit
        };

        public CashInControllerApplicationTest()
        {
            _mapper = AutomapperConfig.GetConfiguration().CreateMapper();

            _logger.Setup(x => x.Log(
               LogLevel.Error,
               It.IsAny<EventId>(),
               It.IsAny<It.IsAnyType>(),
               It.IsAny<Exception>(),
               It.IsAny<Func<It.IsAnyType, Exception?, string>>()));

            _unitOfWork = new UnitOfWork(null, _pspTransactionRepository.Object, _payableRepository.Object, _dataBaseTransaction.Object);

            _cashInService = new CashInService(_logger.Object,
                _feeConfigurationCacheService,
                _unitOfWork,
                _mapper,
                _transactionValidator.Object);

            _controller = new CashInController(_cashInService);
        }

        [Fact]
        [Trait(name: "Integration test CashIn", "Flow Controller Application: ok")]
        public async Task ProcessTransaction_ShouldBeOk()
        {
            //Arrange
            _transactionValidator.Setup(x => x.ValidateAsync(It.IsAny<PspTransaction>(), It.IsAny<CancellationToken>())).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _pspTransactionRepository.Setup(x => x.SaveAsync(It.IsAny<PspTransaction>())).Returns(Task.CompletedTask);
            _payableRepository.Setup(x => x.SaveAsync(It.IsAny<Payable>())).Returns(Task.CompletedTask);

            //Action
            var actionResult = await _controller.ProcessTransaction(_transaction);
            CreatedAtActionResult result = actionResult as CreatedAtActionResult;
            TransactionViewModel vm = (TransactionViewModel)result.Value;

            //Assert
            vm.Should().BeEquivalentTo(_expectedTransaction, opt => opt.Excluding(x => x.TransactionId).Excluding(x => x.Client));
        }

        [Fact]
        [Trait(name: "Integration test CashIn", "Flow Controller Application: Validation Behavior when has errors")]
        public async Task ProcessTransaction_ValidationErrors_ExpectedBadRequest()
        {
            //Arrange
            _transactionValidator.Setup(x => x.ValidateAsync(It.IsAny<PspTransaction>(), It.IsAny<CancellationToken>())).ReturnsAsync(new FluentValidation.Results.ValidationResult()
            {
                Errors = new List<ValidationFailure> {
                    new ValidationFailure { ErrorMessage = "Erro" }
                }
            });
            _pspTransactionRepository.Setup(x => x.SaveAsync(It.IsAny<PspTransaction>())).Returns(Task.CompletedTask);
            _payableRepository.Setup(x => x.SaveAsync(It.IsAny<Payable>())).Returns(Task.CompletedTask);

            //Action
            var actionResult = await _controller.ProcessTransaction(_transaction);


            //Assert
            actionResult.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        [Trait(name: "Integration test CashIn", "Flow Controller Application: Error 500")]
        public async Task ProcessTransaction_DatabaseOut_ExpectedInternalServerErrorAndRollback()
        {
            //Arrange
            _transactionValidator.Setup(x => x.ValidateAsync(It.IsAny<PspTransaction>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            _pspTransactionRepository.Setup(x => x.SaveAsync(It.IsAny<PspTransaction>())).Returns(Task.CompletedTask);
            _payableRepository.Setup(x => x.SaveAsync(It.IsAny<Payable>())).ThrowsAsync(new Exception());

            //Action
            Func<Task> fn = async () => { await _controller.ProcessTransaction(_transaction); };

            //Assert
            fn.Should().ThrowAsync<Exception>();
            _logger.Verify(x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

    }
}
