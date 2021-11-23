using System.Linq.Expressions;

namespace Products.Api.Data.Repositories.Core
{
    public interface IRepository<TEntity> : IAsyncDisposable, IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> Select();
        IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> filter);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entity);
        void Delete(TEntity entity);
        Task SaveAsync();
    }
}
