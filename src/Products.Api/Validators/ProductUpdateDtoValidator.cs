using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

using Products.Api.Dtos;

namespace Products.Api.Validators
{
    public class ProductUpdateDtoValidator : AbstractValidator<JsonPatchDocument<ProductUpdateDto>>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(m => m.Operations)
                .NotNull()
                .NotEmpty();
            RuleForEach(m => m.Operations)
                .NotNull()
                .NotEmpty()
                .ChildRules(o =>
                {
                    o.RuleFor(m => m.op).NotNull().NotEmpty();
                    o.RuleFor(m => m.OperationType).Equal(OperationType.Replace);
                    o.RuleFor(m => m.path).NotNull().NotEmpty().Equal($"/{nameof(ProductUpdateDto.Description).ToLower()}");
                });
        }
    }
}
