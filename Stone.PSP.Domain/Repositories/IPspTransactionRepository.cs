using Stone.PSP.Domain.Entities;

namespace Stone.PSP.Domain.Repositories
{
    public interface IPspTransactionRepository
    {
        Task SaveAsync(PspTransaction pspTransaction);
    }
}
