using Backend.Entities;

namespace Backend.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetQueryable();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(long id);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
