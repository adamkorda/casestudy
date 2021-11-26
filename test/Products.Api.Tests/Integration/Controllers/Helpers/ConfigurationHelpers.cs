using System;
using System.Linq;
using System.Net.Http;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Products.Api.Data;
using Products.Api.Data.Seeder;
using Products.Api.Tests.Helpers.Seeders;

namespace Products.Api.Tests.Integration.Controllers.Helpers
{
    public static class ConfigurationHelpers
    {
        public static HttpClient CreateHttpClientWithEmptyDatabase<TEntryPoint>(WebApplicationFactory<TEntryPoint> webApplicationFactory, string databaseName)
            where TEntryPoint : class
            => CreateHttpClientWithDatabase(webApplicationFactory,
                                            services => services.AddScoped<IDatabaseSeeder, EmptyDatabaseSeeder>(),
                                            databaseName);

        public static HttpClient CreateHttpClientWithSeededDatabase<TEntryPoint>(WebApplicationFactory<TEntryPoint> webApplicationFactory, string databaseName)
            where TEntryPoint : class
            => CreateHttpClientWithDatabase(webApplicationFactory,
                                            services => services.AddScoped<IDatabaseSeeder, TestDatabaseSeeder>(),
                                            databaseName);

        private static HttpClient CreateHttpClientWithDatabase<TEntryPoint>(WebApplicationFactory<TEntryPoint> webApplicationFactory,
                                                                     Action<IServiceCollection> configureServices,
                                                                     string databaseName)
            where TEntryPoint : class
            => webApplicationFactory.WithWebHostBuilder(options
                => options.ConfigureServices(services =>
                {
                    var service = services.Single(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    services.Remove(service);
                    services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(databaseName));
                    configureServices(services);

                })).CreateClient();
    }
}
