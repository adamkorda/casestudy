using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

using Products.Api.Data.Entities;
using Products.Api.Dtos;

using Swashbuckle.AspNetCore.Filters;

namespace Products.Api.Swagger.Examples
{
    public class PatchProductDescriptionExample : IExamplesProvider<object>
    {
        public object GetExamples()
            => new object[]
            {
                new
                {
                    op = "replace",
                    path = $"/{nameof(ProductUpdateDto.Description).ToLower()}",
                    value = "new descrption"
                }
            };
    }
}
