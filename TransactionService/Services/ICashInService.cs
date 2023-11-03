using TransactionService.Help;
using TransactionService.ViewModels;

namespace TransactionService.Services
{
    public interface ICashInService
    {
        Task<IResult<TransactionViewModel>> ProcessTransactionAsync(TransactionViewModel transactionViewModel);
    }
}