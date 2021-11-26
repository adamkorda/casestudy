using System.Linq;

using Products.Api.Data;
using Products.Api.Data.Seeder;

using static Products.Api.Data.Seeder.Utilities.Utils;

namespace Products.Api.Tests.Helpers.Seeders
{
    public class TestDatabaseSeeder : IDatabaseSeeder
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TestDatabaseSeeder(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Seed()
        {
            _applicationDbContext.Database.EnsureDeleted();
            _applicationDbContext.Database.EnsureCreated();
            _applicationDbContext.Products.AddRange(GenerateProducts(1000));
            _applicationDbContext.SaveChanges();
        }
    }
}
