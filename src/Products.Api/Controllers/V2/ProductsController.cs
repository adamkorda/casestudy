using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Products.Api.Data.Repositories.Core;
using Products.Api.Models.Requests;
using Products.Api.Models.Responds;

namespace Products.Api.Controllers.V2
{
    /// <summary>
    /// test
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public ProductsController(IProductRepository productRepository, IMapper mapper, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created item</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductRespond>>> GetProducts(ProductsRequest request)
        {
            _logger.LogInformation("Getting all the products");
            var products = await _productRepository.GetAllProducts();
            return Ok(_mapper.Map<IEnumerable<ProductRespond>>(products));
        }
    }
}
