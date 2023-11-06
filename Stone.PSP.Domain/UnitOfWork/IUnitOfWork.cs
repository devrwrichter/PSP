using Stone.PSP.Domain.Repositories;

namespace Stone.PSP.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPspTransactionRepository PspTransactionRepository { get; }
        IPayableRepository PayableRepository { get; }
        IDatabaseTransaction BeginTransaction();
        Task CommitAsync();
    }
}
