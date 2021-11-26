using AutoMapper;

using FluentAssertions;

using Products.Api.Data.Entities;
using Products.Api.Dtos;
using Products.Api.Mapping;

using Xunit;

using static Products.Api.Tests.Helpers.Categories;

namespace Products.Api.Tests.Unit
{
    [Trait(Category, UnitTest)]
    public class AutomapperProfileTests
    {
        private readonly IMapper _mapper;

        public AutomapperProfileTests()
            => _mapper = new MapperConfiguration(options => options.AddProfile<AutomapperProfile>()).CreateMapper();

        [Fact]
        public void All_properties_of_product_are_fully_mapped_into_available_properties_of_product_dto()
        {
            var source = new Product(10, "name", "uri", 150, "description");
            var destination = _mapper.Map<ProductDto>(source);
            destination.Id.Should().Be(source.Id);
            destination.Name.Should().Be(source.Name);
            destination.ImgUri.Should().Be(source.ImgUri);
            destination.Price.Should().Be(source.Price);
            destination.Description.Should().Be(source.Description);
        }

        [Fact]
        public void All_properties_of_product_are_fully_mapped_into_available_properties_of_product_update_dto()
        {
            var source = new Product(10, "name", "uri", 150, "description");
            var destination = _mapper.Map<ProductUpdateDto>(source);
            destination.Description.Should().Be(source.Description);
        }

        [Fact]
        public void All_properties_of_product_update_dto_are_fully_mapped_into_available_properties_of_product()
        {
            var source = new ProductUpdateDto { Description = "new description" };
            var destination = new Product(10, "name", "uri", 150, "description");
            _mapper.Map(source, destination);
            destination.Description.Should().Be(source.Description);
        }
    }
}
