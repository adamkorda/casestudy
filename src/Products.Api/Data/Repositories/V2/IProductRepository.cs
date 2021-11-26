using Products.Api.Data.Core;
using Products.Api.Data.Entities;
using Products.Api.Data.Repositories.Core;
using Products.Api.Dtos;

namespace Products.Api.Data.Repositories.V2
{
    public interface IProductRepository : IRepository
    {
        Task<PaginatedList<Product>> GetAllProductsAsync(ProductsRequestDto productsRequest);
    }
}
