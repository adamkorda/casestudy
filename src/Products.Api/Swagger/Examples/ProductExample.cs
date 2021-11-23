using Products.Api.Data.Entities;

using Swashbuckle.AspNetCore.Filters;

namespace Products.Api.Swagger.Examples
{
    public class ProductExample : IExamplesProvider<Product>
    {
        public Product GetExamples()
            => new(Guid.NewGuid(), "Some product", "url", 100, null);
    }
}
