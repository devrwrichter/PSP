using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Repositories;
using Stone.PSP.Web.API.Controllers;
using TransactionService.Services;
using TransactionService.ViewModels;

namespace Stone.PSP.Web.API.Test
{
    public class CashOutControllerApplicationTest
    {
        private readonly ICashOutService _cashOutService;
        private readonly Mock<IPayableRepository> _payableRepository;
        private readonly Mock<ILogger<CashOutService>> _logger =  new Mock<ILogger<CashOutService>>();
        private readonly CashOutController _cashOutController;

        public CashOutControllerApplicationTest()
        {
            _logger.Setup(x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()));

            _payableRepository = new Mock<IPayableRepository>();
            _cashOutService = new CashOutService(_payableRepository.Object, _logger.Object);
            _cashOutController = new CashOutController(_cashOutService);
        }

        [Fact]
        [Trait(name:"Integration test Cashout", "Flow Controller Application: OK")]
        public void GetBalance_ShouldBeOk()
        {
            //Arrange
            var clientBalance = new ClientBalance { Available = 10.0M, WaitingFunds = 8.0M };
            var clientBalanceExpected = new ClientBalanceViewModel { 
                Available = clientBalance.Available, WaitingFunds = clientBalance.WaitingFunds 
            };
            _payableRepository.Setup(x => x.GetBalanceAsync(It.IsAny<Guid>())).ReturnsAsync(clientBalance);


            //Action
            var actionResult = _cashOutController.GetBalanceAsync(Guid.NewGuid());

            //Assert
           actionResult.Result.Should().BeOfType<OkObjectResult>();
           ((OkObjectResult)actionResult.Result).Value.Should().Be(clientBalanceExpected);
        }

        [Fact]
        [Trait(name: "Integration test cashOut", "Flow Controller Application: NotFound")]
        public void GetBalance_NotFoundInDatabase_ShouldBeOk()
        {
            //Arrange
            ClientBalance clientBalance = null;            
            _payableRepository.Setup(x => x.GetBalanceAsync(It.IsAny<Guid>())).ReturnsAsync(clientBalance);

            //Action
            var actionResult = _cashOutController.GetBalanceAsync(Guid.NewGuid());

            //Assert
            actionResult.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        [Trait(name: "Integration test cashOut", "Flow Controller Application: 500")]
        public void GetBalance_ThrowExceptionOnRepository_ShouldBeOk()
        {
            //Arrange
            var ex = new Exception();
            _payableRepository.Setup(x => x.GetBalanceAsync(It.IsAny<Guid>())).ThrowsAsync(ex);

            //Action
            Func<Task> fn = async () => { await _cashOutController.GetBalanceAsync(Guid.NewGuid()); };

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