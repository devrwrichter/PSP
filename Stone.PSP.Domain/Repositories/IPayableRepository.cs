using Stone.PSP.Domain.Entities;

namespace Stone.PSP.Domain.Repositories
{
    public interface IPayableRepository
    {
        Task<ClientBalance> GetBalanceAsync(Guid clientId);
        Task SaveAsync(Payable payable);
    }
}
