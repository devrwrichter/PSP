using Stone.PSP.Domain.Repositories;
using Stone.PSP.Domain.UnitOfWork;
using Stone.PSP.Infra.Context;

namespace Stone.PSP.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentContext _context;
        public IPspTransactionRepository PspTransactionRepository { get; }//TODO: Make readonly
        public IPayableRepository PayableRepository { get; }

        public readonly IDatabaseTransaction _databaseTransaction;

        public UnitOfWork(PaymentContext context, IPspTransactionRepository pspTransactionRepository,
            IPayableRepository payableRepository,
            IDatabaseTransaction dataBaseTransaction)
        {
            _context = context;
            PspTransactionRepository = pspTransactionRepository;
            PayableRepository = payableRepository;
            _databaseTransaction = dataBaseTransaction;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IDatabaseTransaction BeginTransaction()
        {
            return _databaseTransaction;
        }
    }
}
