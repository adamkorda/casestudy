using Microsoft.EntityFrameworkCore;

using Products.Api.Data.Entities;
using Products.Api.Data.Repositories.Core;

namespace Products.Api.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext) { }

        public void CreateProduct(Product product) => Create(product);
        public void DeleteProduct(Product product) => Delete(product);
        public async Task<IEnumerable<Product>> GetAllProducts() => await Select().ToListAsync();
        public async Task<Product?> GetProductById(Guid id) => await Select(p => p.Id == id).FirstOrDefaultAsync();
        public void UpdateProduct(Product product) => Update(product);
    }
}
