using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace Products.Api.Data.Repositories.Core
{
    public abstract class RepositoryBase<TEntity> : IRepository
        where TEntity : class
    {
        protected readonly DbSet<TEntity> _entities;
        protected readonly DbContext _dbContext;
        private bool _disposedValue;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<TEntity>();
        }

        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

        public IQueryable<TEntity> Select() => _entities.AsNoTracking();

        public IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> filter, bool trackChanges)
            => trackChanges ? _entities.Where(filter) : _entities.Where(filter).AsNoTracking();

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposedValue = true;
            }
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (_dbContext is not null)
            {
                await _dbContext.DisposeAsync();
            }
        }
    }
}
