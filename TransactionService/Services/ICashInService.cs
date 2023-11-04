using TransactionService.Help;
using TransactionService.ViewModels;

namespace TransactionService.Services
{
    public interface ICashInService
    {
        Task<TransactionViewModel?> GetTransactionByIdAsync(Guid id);
        Task<IResult<TransactionViewModel>> ProcessTransactionAsync(TransactionViewModel transactionViewModel);
    }
}