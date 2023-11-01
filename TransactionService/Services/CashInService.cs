using CSharpFunctionalExtensions;
using Stone.PSP.Domain.Entities;
using TransactionService.ViewModels;

namespace TransactionService.Services
{
    public class CashInService
    {
        public async Task<Result<TransactionViewModel>> SaveTransactionAsync(TransactionViewModel transactionViewModel){

            Result<TransactionViewModel> result = new();

            var transaction = GetTransactionByViewModel(transactionViewModel);


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
                CardNumber = transactionViewModel.CreditCard.CardNumber.Trim(),
                CardHolder = transactionViewModel.CreditCard.CardHolder.Trim(),
                CardExpirationDate = transactionViewModel.CreditCard.CardValidateDate,
                CardVerificationCode = transactionViewModel.CreditCard.CardVerificationCode,
            };
        }
    }
}
