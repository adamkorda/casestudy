using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Products.Api.Data.Core;
using Products.Api.Data.Repositories.Products.V2;
using Products.Api.Dtos;

namespace Products.Api.Controllers.Products.V2
{
    /// <summary>
    /// Products controller
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/products")]
    [Route("api/v{version:apiVersion}/products")]
    [Produces("application/json")]
    public class ProductsV2Controller : ControllerBase
    {
        private readonly IProductRepositoryV2 _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsV2Controller> _logger;

        public ProductsV2Controller(IProductRepositoryV2 productRepository, IMapper mapper, ILogger<ProductsV2Controller> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets all products with pagination
        /// </summary>
        /// <returns>Collection of products</returns>
        /// <response code="200">Collection of products</response>
        /// <response code="500">Error due to server failure (e.g. database connectivity)</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] ProductsRequestDto productsRequest)
        {
            _logger.LogInformation("Getting all the products with pagination");
            var products = await _productRepository.GetAllProductsAsync(productsRequest);
            if (!products.Any())
            {
                _logger.LogInformation("There are no products stored in database");
            }

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(_mapper.Map<PaginationMetadataDto>(products.Metadata)));

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }
    }
}
