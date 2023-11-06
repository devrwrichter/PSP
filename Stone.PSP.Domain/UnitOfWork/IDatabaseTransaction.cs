namespace Stone.PSP.Domain.UnitOfWork
{
    public interface IDatabaseTransaction : IDisposable
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}