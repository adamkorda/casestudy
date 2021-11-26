using Microsoft.EntityFrameworkCore;

using Products.Api.Data.Entities;
using Products.Api.Data.Repositories.Core;

namespace Products.Api.Data.Repositories.V1
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() => await Select().ToListAsync();
        public async Task<Product?> GetProductByIdAsync(int id, bool trackChanges = false) => await Select(p => p.Id == id, trackChanges).FirstOrDefaultAsync();
    }
}