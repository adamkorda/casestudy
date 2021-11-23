
using Microsoft.EntityFrameworkCore;

using Products.Api.Data;

public static class AppDbContextExtensions
{
    public static IHost MigrateDatabase(this IHost host, bool doSeedDatabase = false)
    {
        using var scope = host.Services.CreateScope();
        using var appDbContext = scope.ServiceProvider.GetService<AppDbContext>();
        appDbContext?.Database.Migrate();
        return host;
    }
}
