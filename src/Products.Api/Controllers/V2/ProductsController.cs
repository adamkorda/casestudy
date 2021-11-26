using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Products.Api.Data.Repositories.V2;
using Products.Api.Dtos;

namespace Products.Api.Controllers.V2
{
    /// <summary>
    /// Products controller
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository productRepository, IMapper mapper, ILogger<ProductsController> logger)
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] ProductsRequestDto productsRequest)
        {
            _logger.LogInformation("Getting all the products with pagination");
            var products = await _productRepository.GetAllProductsAsync(productsRequest);
            if (!products.Any())
            {
                _logger.LogInformation("There are no products stored in database");
            }

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.Metadata));

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }
    }
}
