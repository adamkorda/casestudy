
using System;

using FluentAssertions;

using Products.Api.Data;
using Products.Api.Data.Seeder;
using Products.Api.Tests.Helpers.Fixtures;

using Xunit;

using static Products.Api.Tests.Helpers.Categories;

namespace Products.Api.Tests.Unit
{
    [Trait(Category, UnitTest)]
    public class DatabaseSeederTests : IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DatabaseSeederTests()
            => _applicationDbContext = new InMemoryApplicationDbContextFixture().ApplicationDbContext;

        public void Dispose() => _applicationDbContext.Dispose();

        [Fact]
        public void Database_will_contain_initial_records_after_seeding()
        {
            _applicationDbContext.Products.Should().BeNullOrEmpty();

            var sut = new DatabaseSeeder(_applicationDbContext);
            sut.Seed();

            _applicationDbContext.Products.Should().NotBeNullOrEmpty().And.HaveCount(100);
        }
    }
}
