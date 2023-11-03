using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Repositories;
using Stone.PSP.Infra.Context;

namespace Stone.PSP.Infra.Repositories
{
    public class PspTransactionRepository : IPspTransactionRepository
    {
        private readonly PaymentContext _context;

        public PspTransactionRepository(PaymentContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(PspTransaction pspTransaction)
        {
            await _context.Transactions.AddAsync(pspTransaction);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
