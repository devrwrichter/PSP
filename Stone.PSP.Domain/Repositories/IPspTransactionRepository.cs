using Stone.PSP.Crosscutting;
using Stone.PSP.Domain.Entities;

namespace Stone.PSP.Domain.Repositories
{
    public interface IPspTransactionRepository
    {
        Task<PspTransaction?> GetTransactionByIdAsync(Guid id);
        Task<IList<PspTransaction>> GetTransactionsAsync(Pagination pagination);
        Task SaveAsync(PspTransaction pspTransaction);
    }
}
