using Stone.PSP.Crosscutting;
using Stone.PSP.Domain.Entities;

namespace Stone.PSP.Domain.Repositories
{
    public interface IPspTransactionRepository
    {
        Task<Transaction?> GetTransactionByIdAsync(Guid id);
        Task<(int Count, IEnumerable<Transaction> Items)> GetTransactionsAsync(PaginationRequest pagination);
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
        Task SaveAsync(Transaction pspTransaction);
    }
}
