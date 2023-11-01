using Stone.PSP.Domain.Repositories;
using Stone.PSP.Infra.Context;

namespace Stone.PSP.Infra.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PaymentContext _context;

        public TransactionRepository(PaymentContext context)
        {
            _context = context;
        }
    }
}
