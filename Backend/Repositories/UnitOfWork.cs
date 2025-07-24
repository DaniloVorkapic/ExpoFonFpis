using Backend.Data;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CommitAsync()
        {
            UpdateState();
            return _dbContext.SaveChangesAsync();
        }

        private void UpdateState()
        {
            var entityEntries = _dbContext.ChangeTracker.Entries<BaseEntity>();
            foreach (var entityEntry in entityEntries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Entity.CreatedAt = DateTime.Now;
                    entityEntry.Entity.UpdatedAt = DateTime.Now;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Entity.UpdatedAt = DateTime.Now;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposed = true;
            }
        }

    }
}
