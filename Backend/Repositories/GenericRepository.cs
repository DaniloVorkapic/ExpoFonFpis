using Backend.Data;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> CreateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            return await Task.FromResult(true);
        }

    }
}