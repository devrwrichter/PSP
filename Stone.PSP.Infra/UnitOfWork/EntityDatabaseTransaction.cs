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
        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}
