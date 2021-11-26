using Products.Api.Data.Core;
using Products.Api.Data.Core.Extensions;
using Products.Api.Data.Entities;
using Products.Api.Data.Repositories.Core;
using Products.Api.Dtos;

namespace Products.Api.Data.Repositories.Products.V2
{
    public class ProductRepositoryV2 : RepositoryBase<Product>, IProductRepositoryV2
    {
        public ProductRepositoryV2(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<PaginatedList<Product>> GetAllProductsAsync(ProductsRequestDto productsRequest)
            => await Select().ToPaginatedListAsync(productsRequest.PageNumber, productsRequest.PageSize);
    }
}