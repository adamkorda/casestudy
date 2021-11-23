using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace Products.Api.Tests
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<ProductsControllerTests>>
    {
        private readonly WebApplicationFactory<ProductsControllerTests> _factory;

        public ProductsControllerTests(WebApplicationFactory<ProductsControllerTests> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void Get_All_Products_Will_Return()
        {            
        }
    }
}