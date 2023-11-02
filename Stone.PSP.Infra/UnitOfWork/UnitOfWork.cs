using Stone.PSP.Domain.Repositories;
using Stone.PSP.Domain.UnitOfWork;
using Stone.PSP.Infra.Context;

namespace Stone.PSP.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentContext _context;
        public IPspTransactionRepository PspTransactionRepository { get; }
        public IPayableRepository PayableRepository { get; }

        public UnitOfWork(PaymentContext context, IPspTransactionRepository pspTransactionRepository, IPayableRepository payableRepository)
        {
            _context = context;
            PspTransactionRepository = pspTransactionRepository;
            PayableRepository = payableRepository;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IDatabaseTransaction BeginTransaction()
        {
            return new EntityDatabaseTransaction(_context);
        }
    }
}
