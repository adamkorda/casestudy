
using Products.Api.Data.Core;
using Products.Api.Data.Core.Extensions;
using Products.Api.Data.Entities;
using Products.Api.Data.Repositories.Core;
using Products.Api.Dtos;

namespace Products.Api.Data.Repositories.V2
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<PaginatedList<Product>> GetAllProductsAsync(ProductsRequestDto productsRequest)
            => await Select().ToPaginatedListAsync(productsRequest.PageNumger, productsRequest.PageSize);
    }
}