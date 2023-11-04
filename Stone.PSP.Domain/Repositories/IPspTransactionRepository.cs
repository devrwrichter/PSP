using Stone.PSP.Domain.Entities;

namespace Stone.PSP.Domain.Repositories
{
    public interface IPspTransactionRepository
    {
        Task<PspTransaction?> GetTransactionByIdAsync(Guid id);
        Task SaveAsync(PspTransaction pspTransaction);
    }
}
