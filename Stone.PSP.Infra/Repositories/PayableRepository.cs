using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Repositories;

namespace Stone.PSP.Infra.Repositories
{
    public class PayableRepository : IPayableRepository
    {
        public Task SaveAsync(Payable payable)
        {
            throw new NotImplementedException();
        }
    }
}
