namespace Stone.PSP.Domain.UnitOfWork
{
    public interface IDatabaseTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}