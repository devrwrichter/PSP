using Stone.PSP.Crosscutting;
using TransactionService.Help;
using TransactionService.ViewModels;

namespace TransactionService.Services
{
    public interface ICashInService
    {
        Task<TransactionViewModel?> GetTransactionByIdAsync(Guid id);
        Task<ICollection<TransactionViewModel>> GetTransactionsAsync();
        Task<PageResponse<TransactionViewModel>> GetTransactionsWithPaginationAsync(PaginationRequest pagination);
        Task<IResult<TransactionViewModel>> ProcessTransactionAsync(TransactionViewModel transactionViewModel);
    }
}