using Stone.PSP.Crosscutting;
using TransactionService.Help;
using TransactionService.ViewModels;

namespace TransactionService.Services
{
    public interface ICashInService
    {
        Task<TransactionViewModel?> GetTransactionByIdAsync(Guid id);
        Task<IList<TransactionViewModel>> GetTransactionsAsync(Pagination pagination);
        Task<IResult<TransactionViewModel>> ProcessTransactionAsync(TransactionViewModel transactionViewModel);
    }
}