using Stone.PSP.Domain.Entities;

namespace Stone.PSP.Domain.Repositories
{
    public interface IPayableRepository
    {
        Task SaveAsync(Payable payable);
    }
}
