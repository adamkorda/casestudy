using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Products.Api.Controllers.V2;
using Products.Api.Data;
using Products.Api.Data.Seeder;
using Products.Api.Tests.Helpers.Models;
using Products.Api.Tests.Helpers.Seeders;

using Xunit;

namespace Products.Api.Tests.Integration.Controllers.V2
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<ProductsController>>
    {
        private readonly HttpClient _httpClient;

        public ProductsControllerTests(WebApplicationFactory<ProductsController> webApplicationFactory)
        {
            _httpClient = webApplicationFactory.WithWebHostBuilder(options =>
            {
                options.ConfigureServices(services =>
                {
                    ConfigureInMemoryApplicationDbContext(services);
                    services.AddScoped<IDatabaseSeeder, TestDatabaseSeeder>();
                });
            }).CreateClient();
        }

        [Fact]
        public async Task Getting_all_products_will_return_empty_collection_when_there_are_no_records_in_database()
        {
            var client = new WebApplicationFactory<ProductsController>()
                .WithWebHostBuilder(options =>
                {
                    options.ConfigureServices(services =>
                    {
                        ConfigureInMemoryApplicationDbContext(services);
                        services.AddScoped<IDatabaseSeeder, EmptyDatabaseSeeder>();

                    });
                }).CreateClient();

            var result = await client.GetFromJsonAsync<IEnumerable<ProductRespond>>("api/v2/products");
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Getting_one_product_by_its_identifier_will_return_matched_product_record()
        {
            var productId = 1;
            var result = await _httpClient.GetFromJsonAsync<ProductRespond>($"api/v2/products/{productId}");
            result.Should().NotBeNull();
            result!.Id.Should().Be(1);
        }


        private static void ConfigureInMemoryApplicationDbContext(IServiceCollection services)
        {
            var service = services.Single(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            services.Remove(service);
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("CaseStudyV2"));
        }
    }
}