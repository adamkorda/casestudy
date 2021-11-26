using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using Products.Api.Controllers.Products.V1;
using Products.Api.Tests.Helpers.Models;

using Xunit;

using static Products.Api.Tests.Helpers.Categories;
using static Products.Api.Tests.Integration.Controllers.Helpers.ConfigurationHelpers;

namespace Products.Api.Tests.Integration.Controllers.Products.V1
{
    [Trait(Category, IntegrationTest)]
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<ProductsController>>
    {
        const string DatabaseName = "CaseStudyV1";
        private readonly HttpClient _httpClient;

        public static IEnumerable<object[]> InvalidPatchDescriptionRequests => new List<object[]>
        {
            new object[] { null! },
            new object[]
            {
                Array.Empty<object>()
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
            => _httpClient = CreateHttpClientWithSeededDatabase(webApplicationFactory, DatabaseName);

        [Fact]
        public async Task Getting_all_products_will_return_empty_collection_when_there_are_no_records_in_database()
        {
            var client = CreateHttpClientWithEmptyDatabase(new WebApplicationFactory<ProductsController>(), DatabaseName);

            var result = await client.GetFromJsonAsync<IEnumerable<ProductRespond>>("api/products");
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Getting_one_product_by_its_identifier_will_return_not_found_when_there_are_no_records_in_database()
        {
            var client = CreateHttpClientWithEmptyDatabase(new WebApplicationFactory<ProductsController>(), DatabaseName);

            var productId = 1;
            var result = await client.GetAsync($"api/products/{productId}");
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Updating_description_of_product_will_return_not_found_when_there_are_no_records_in_database()
        {
            var client = CreateHttpClientWithEmptyDatabase(new WebApplicationFactory<ProductsController>(), DatabaseName);

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
        public async Task Updating_description_of_product_will_return_bad_request_when_using_invalid_request(object invalidPatchRequest)
        {
            var productId = 1;
            var result = await _httpClient.PatchAsync($"api/products/{productId}", JsonContent.Create(invalidPatchRequest));
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Updating_description_of_product_will_return_no_content_when_product_is_successfully_updated()
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
            product!.Description.Should().Be("new description");
        }
    }
}