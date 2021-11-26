using Products.Api.Data.Core;
using Products.Api.Data.Entities;
using Products.Api.Data.Repositories.Core;
using Products.Api.Dtos;

namespace Products.Api.Data.Repositories.Products.V2
{
    public interface IProductRepositoryV2 : IRepository
    {
        Task<PaginatedList<Product>> GetAllProductsAsync(ProductsRequestDto productsRequest);
    }
}
