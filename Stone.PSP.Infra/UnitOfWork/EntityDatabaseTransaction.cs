using Microsoft.EntityFrameworkCore.Storage;
using Stone.PSP.Infra.Context;

namespace Stone.PSP.Domain.UnitOfWork
{
    public class EntityDatabaseTransaction : IDatabaseTransaction
    {
        private readonly IDbContextTransaction _transaction;

        public EntityDatabaseTransaction(PaymentContext context)
        {
                _transaction = context.Database.BeginTransaction();
        }
        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}
