using FluentAssertions;

using Products.Api.Data;
using Products.Api.Data.Seeder;
using Products.Api.Tests.Helpers.Fixtures;

using Xunit;

namespace Products.Api.Tests.Unit
{
    public class DatabaseSeederTests
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DatabaseSeederTests()
        {
            _applicationDbContext = new InMemoryApplicationDbContextFixture().ApplicationDbContext;
        }

        [Fact]
        public void Database_will_contain_initial_records_after_seeding()
        {
            var sut = new DatabaseSeeder(_applicationDbContext);
            sut.Seed();
            _applicationDbContext.Products.Should().NotBeNullOrEmpty().And.HaveCount(100);
        }
    }
}
