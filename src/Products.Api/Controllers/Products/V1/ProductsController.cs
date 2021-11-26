using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using Products.Api.Data.Repositories.Products.V1;
using Products.Api.Dtos;
using Products.Api.Swagger.Examples;

using Swashbuckle.AspNetCore.Filters;

namespace Products.Api.Controllers.Products.V1
{
    /// <summary>
    /// Products controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/products")]
    [Route("api/v{version:apiVersion}/products")]
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
        /// Gets all products
        /// </summary>
        /// <returns>Collection of products</returns>
        /// <response code="200">Collection of products</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            _logger.LogInformation("Getting all the products");
            var products = await _productRepository.GetAllProductsAsync();
            if (!products.Any())
            {
                _logger.LogInformation("There are no products stored in database");
            }

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        /// <summary>
        /// Gets one product by its id
        /// </summary>
        /// <returns>Single product</returns>
        /// <response code="200">Successfully found product</response>
        /// <response code="404">Product was not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            _logger.LogInformation($"Getting one product by id: {id}");
            var product = await _productRepository.GetProductByIdAsync(id, false);

            if (product == null)
            {
                _logger.LogInformation($"Product with id: {id} does not exist in database");
                return NotFound();
            }

            return Ok(_mapper.Map<ProductDto>(product));
        }

        /// <summary>
        /// Updates description of product by its id
        /// </summary>
        /// <returns>Single product</returns>
        /// <response code="204">Product was successfully updated</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Product was not found</response>
        /// <response code="415">Request content type must be application/json-patch+json</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [SwaggerRequestExample(typeof(JsonPatchDocument<ProductUpdateDto>), typeof(PatchProductDescriptionExample))]
        public async Task<ActionResult<ProductDto>> UpdateProductDescription(int id, [FromBody] JsonPatchDocument<ProductUpdateDto> patchDoc)
        {
            _logger.LogInformation($"Updating product: {id} description");

            var product = await _productRepository.GetProductByIdAsync(id, true);
            if (product == null)
            {
                _logger.LogInformation($"Product with id: {id} does not exist in database");
                return NotFound();
            }

            var productToPatch = _mapper.Map<ProductUpdateDto>(product);
            patchDoc.ApplyTo(productToPatch);
            _mapper.Map(productToPatch, product);
            await _productRepository.SaveAsync();

            return NoContent();
        }
    }
}
