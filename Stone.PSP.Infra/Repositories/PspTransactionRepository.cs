using Microsoft.EntityFrameworkCore;
using Stone.PSP.Crosscutting;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Repositories;
using Stone.PSP.Infra.Context;

namespace Stone.PSP.Infra.Repositories
{
    public class PspTransactionRepository : IPspTransactionRepository
    {
        private readonly PaymentContext _context;
        private readonly PaymentDapperContext _dapperContext;

        public PspTransactionRepository(PaymentContext context, PaymentDapperContext dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;
        }

        public async Task<PspTransaction?> GetTransactionByIdAsync(Guid id)
        {
            return await _context.Transactions
                .Where(x => x.Id == id)
                .AsNoTracking()
                .TagWith(nameof(GetTransactionByIdAsync))
                .FirstOrDefaultAsync();
        }

        public Task<IList<PspTransaction>> GetTransactionsAsync(Pagination pagination)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync(PspTransaction pspTransaction)
        {
            await _context.Transactions.AddAsync(pspTransaction);
            await _context.SaveChangesAsync();
        }
    }
}
