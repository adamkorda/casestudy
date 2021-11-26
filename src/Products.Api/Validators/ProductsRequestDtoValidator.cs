using FluentValidation;

using Products.Api.Dtos;

namespace Products.Api.Validators
{
    public class ProductsRequestDtoValidator : AbstractValidator<ProductsRequestDto>
    {
        public ProductsRequestDtoValidator()
        {
            RuleFor(m => m.PageNumber).GreaterThan(0);
            RuleFor(m => m.PageSize).GreaterThan(0);
        }
    }
}
