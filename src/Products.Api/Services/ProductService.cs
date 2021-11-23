
using Products.Api.Data.Entities;
using Products.Api.Data.Repositories.Core;
using Products.Api.Models.Requests;
using Products.Api.Models.Responds;

namespace Products.Api.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            _logger.LogInformation("Getting all the products");
            return await _productRepository.GetAllProducts();
        }

        public async Task<IEnumerable<Product>> GetProducts(ProductsRequest request)
        {
            _logger.LogInformation("Getting all the products");
            return await _productRepository.GetAllProducts();
        }
    }
}
