
using Products.Api.Data;
using Products.Api.Data.Seeder;

namespace Products.Api.Tests.Helpers.Seeders
{
    public class EmptyDatabaseSeeder : IDatabaseSeeder
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmptyDatabaseSeeder(ApplicationDbContext applicationDbContext)
            => _applicationDbContext = applicationDbContext;

        public void Seed()
        {
            _applicationDbContext.Database.EnsureDeleted();
            _applicationDbContext.Database.EnsureCreated();
        }
    }
}
