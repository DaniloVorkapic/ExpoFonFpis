namespace Backend.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}
