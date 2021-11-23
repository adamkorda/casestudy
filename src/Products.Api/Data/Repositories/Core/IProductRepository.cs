using Products.Api.Data.Entities;

namespace Products.Api.Data.Repositories.Core
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product?> GetProductById(Guid id);
        void CreateProduct(Product product);
        Task SaveAsync();
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
    }
}
