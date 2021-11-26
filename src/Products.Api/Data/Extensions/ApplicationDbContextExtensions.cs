using Products.Api.Data.Seeder;

namespace Products.Api.Data.Extensions
{
    public static class ApplicationDbContextExtensions
    {
        public static IHost SeedDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var databaseSeeder = scope.ServiceProvider.GetService<IDatabaseSeeder>();
            databaseSeeder?.Seed();
            return host;
        }
    }
}
