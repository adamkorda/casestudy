using Products.Api.Data.Entities;
using Products.Api.Data.Repositories.Core;

namespace Products.Api.Data.Repositories.Products.V1
{
    public interface IProductRepository : IRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(int id, bool trackChanges = false);
    }
}
