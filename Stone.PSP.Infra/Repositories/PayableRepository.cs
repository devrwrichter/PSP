using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Repositories;
using Stone.PSP.Infra.Context;

namespace Stone.PSP.Infra.Repositories
{
    public class PayableRepository : IPayableRepository
    {
        private readonly PaymentContext _context;

        public PayableRepository(PaymentContext context)
        {
            _context = context;
        }
        public async Task SaveAsync(Payable payable)
        {
            await _context.Payables.AddAsync(payable);
            await _context.SaveChangesAsync();
        }
    }
}
