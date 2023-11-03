using CSharpFunctionalExtensions;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Services;
using Stone.PSP.Domain.UnitOfWork;
using TransactionService.ViewModels;
using Stone.PSP.Domain.GLPD;
using FluentValidation;

namespace TransactionService.Services
{
    public class CashInService : ICashInService
    {
        private IPayableDomainService _payableDomainService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<PspTransaction> _transactionValidator;

        public CashInService(IUnitOfWork unitOfWork, IPayableDomainService payableDomainService, IValidator<PspTransaction> transactionValidator)
        {
            _unitOfWork = unitOfWork;
            _payableDomainService = payableDomainService;
            _transactionValidator = transactionValidator;
        }

        public async Task<Result> ProcessTransactionAsync(TransactionViewModel transactionViewModel)
        {
            Result<TransactionViewModel> result = new();

            var pspTransaction = GetTransactionByViewModel(transactionViewModel);
            var payable = _payableDomainService.GetPayable(pspTransaction);

            //validate
            var transactionResult = await _transactionValidator.ValidateAsync(pspTransaction);
            if (!transactionResult.IsValid)
            {
                //TODO
            }

            using(var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    await _unitOfWork.PspTransactionRepository.SaveAsync(pspTransaction);
                    await _unitOfWork.PayableRepository.SaveAsync(payable);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            return result;
        }

        private PspTransaction GetTransactionByViewModel(TransactionViewModel transactionViewModel)
        {
            return new PspTransaction
            {
                ClientId = transactionViewModel.Client.Id,
                PaymentMethodCode = transactionViewModel.PaymentMethodCode,
                Value = transactionViewModel.Value,
                Description = transactionViewModel.Description?.Trim(),
                CardNumber = transactionViewModel.CreditCard.CardNumber.Trim().GetOfuscatedCreditCardNumber(),//TODO
                CardHolder = transactionViewModel.CreditCard.CardHolder.Trim(),
                CardExpirationDate = transactionViewModel.CreditCard.CardValidateDate,
                CardVerificationCode = transactionViewModel.CreditCard.CardVerificationCode
            };
        }
    }
}
