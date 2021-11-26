namespace Products.Api.Data.Repositories.Core
{
    public interface IRepository : IAsyncDisposable, IDisposable
    {
        Task SaveAsync();
    }
}
