using static Products.Api.Data.Seeder.Utilities.Utils;

namespace Products.Api.Data.Seeder
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DatabaseSeeder(ApplicationDbContext applicationDbContext)
            => _applicationDbContext = applicationDbContext;

        public void Seed()
        {
            _applicationDbContext.Database.EnsureCreated();
            if (!_applicationDbContext.Products.Any())
            {
                _applicationDbContext.Products.AddRange(GenerateProducts(100));
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
