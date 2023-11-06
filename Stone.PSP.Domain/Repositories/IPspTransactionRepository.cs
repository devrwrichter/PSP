using Stone.PSP.Crosscutting;
using Stone.PSP.Domain.Entities;

namespace Stone.PSP.Domain.Repositories
{
    public interface IPspTransactionRepository
    {
        Task<PspTransaction?> GetTransactionByIdAsync(Guid id);
        Task<(int Count, IEnumerable<PspTransaction> Items)> GetTransactionsAsync(PaginationRequest pagination);
        Task<IEnumerable<PspTransaction>> GetTransactionsAsync();
        Task SaveAsync(PspTransaction pspTransaction);
    }
}
