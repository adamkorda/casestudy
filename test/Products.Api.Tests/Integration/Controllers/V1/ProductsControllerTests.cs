using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Products.Api.Controllers.V1;
using Products.Api.Data;
using Products.Api.Data.Seeder;
using Products.Api.Tests.Helpers.Models;
using Products.Api.Tests.Helpers.Seeders;

using Xunit;

namespace Products.Api.Tests.Integration.Controllers.V1
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<ProductsController>>
    {
        private readonly HttpClient _httpClient;

        public static IEnumerable<object[]> InvalidPatchDescriptionRequests => new List<object[]>
        {
            new object[] { null! },
            new object[]
            {
                new object[] { }
            },
            new object[]
            {
                new object[] { null! }
            },
            new object[]
            {
                new object[]
                {
                    new
                    {
                        op = "remove",
                        path = "/description",
                        value = "new description"
                    }
                }
            },
            new object[]
            {
                new object[]
                {
                    new
                    {
                        op = "replace",
                        path = "/notexistingpath",
                        value = "new description"
                    }
                }
            }
        };

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

            var result = await client.GetFromJsonAsync<IEnumerable<ProductRespond>>("api/products");
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Getting_one_product_by_its_identifier_will_return_not_found_when_there_are_no_records_in_database()
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

            var productId = 1;
            var result = await client.GetAsync($"api/products/{productId}");
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Updating_description_of_product_will_return_not_found_when_there_are_no_records_in_database()
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

            var productId = 1;
            var productDescriptionPatchRequest = new object[]
            {
                new
                {
                    op = "replace",
                    path = "/description",
                    value = "new description"
                }
            };

            var result = await client.PatchAsync($"api/products/{productId}", JsonContent.Create(productDescriptionPatchRequest));
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Getting_all_products_will_return_all_stored_product_records()
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<ProductRespond>>("api/products");
            result.Should().NotBeEmpty().And.HaveCount(1000);
        }

        [Fact]
        public async Task Getting_one_product_by_its_identifier_will_return_not_found_when_product_is_not_present_in_database()
        {
            var productId = -1;
            var result = await _httpClient.GetAsync($"api/products/{productId}");
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Getting_one_product_by_its_identifier_will_return_matched_product_record()
        {
            var productId = 1;
            var result = await _httpClient.GetFromJsonAsync<ProductRespond>($"api/products/{productId}");
            result.Should().NotBeNull();
            result!.Id.Should().Be(1);
        }

        [Theory]
        [MemberData(nameof(InvalidPatchDescriptionRequests))]
        public async Task Updating_description_of_product(object invalidPatchRequest)
        {
            var productId = 1;
            var result = await _httpClient.PatchAsync($"api/products/{productId}", JsonContent.Create(invalidPatchRequest));
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Successfully_updating_description_of_product()
        {
            var productId = 1;
            var productDescriptionPatchRequest = new object[]
            {
                new
                {
                    op = "replace",
                    path = "/description",
                    value = "new description"
                }
            };

            var result = await _httpClient.PatchAsync($"api/products/{productId}", JsonContent.Create(productDescriptionPatchRequest));
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var product = await _httpClient.GetFromJsonAsync<ProductRespond>($"api/products/{productId}");
            product.Description.Should().Be("new description");
        }

        private static void ConfigureInMemoryApplicationDbContext(IServiceCollection services)
        {
            var service = services.Single(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            services.Remove(service);
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("CaseStudyV1"));
        }
    }
}