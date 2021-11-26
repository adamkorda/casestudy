using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using Newtonsoft.Json;

using Products.Api.Controllers.Products.V2;
using Products.Api.Tests.Helpers.Models;

using Xunit;

using static Products.Api.Tests.Helpers.Categories;
using static Products.Api.Tests.Integration.Controllers.Helpers.ConfigurationHelpers;

namespace Products.Api.Tests.Integration.Controllers.Products.V2
{
    [Trait(Category, IntegrationTest)]
    public class ProductsV2ControllerTests : IClassFixture<WebApplicationFactory<ProductsV2Controller>>
    {
        const string DatabaseName = "CaseStudyV2";
        private readonly HttpClient _httpClient;

        public ProductsV2ControllerTests(WebApplicationFactory<ProductsV2Controller> webApplicationFactory)
            => _httpClient = CreateHttpClientWithSeededDatabase(webApplicationFactory, DatabaseName);

        [Fact]
        public async Task Getting_all_products_will_return_empty_collection_when_there_are_no_records_in_database()
        {
            var client = CreateHttpClientWithEmptyDatabase(new WebApplicationFactory<ProductsV2Controller>(), DatabaseName);

            var result = await client.GetFromJsonAsync<IEnumerable<ProductRespond>>("api/v2/products");
            result.Should().BeEmpty();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task Getting_all_products_will_return_bad_request_when_page_number_is_less_then_one(int pageNumber)
        {
            var result = await _httpClient.GetAsync($"api/v2/products?pagenumber={pageNumber}");
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task Getting_all_products_will_return_bad_request_when_page_size_is_less_then_one(int pageSize)
        {
            var result = await _httpClient.GetAsync($"api/v2/products?pagesize={pageSize}");
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Getting_all_products_will_return_first_page_with_10_product_records_when_no_pagination_is_specified()
        {
            var respond = await _httpClient.GetAsync("api/v2/products");

            respond.StatusCode.Should().Be(HttpStatusCode.OK);
            respond.Headers.Contains("X-Pagination").Should().BeTrue();

            var paginationMetadata = JsonConvert.DeserializeObject<PaginationMetadata>(respond.Headers.GetValues("X-Pagination").First());
            paginationMetadata!.PageNumber.Should().Be(1);
            paginationMetadata.TotalPages.Should().Be(100);
            paginationMetadata.HasNext.Should().BeTrue();
            paginationMetadata.HasPrevious.Should().BeFalse();

            var result = await respond.Content.ReadFromJsonAsync<IEnumerable<ProductRespond>>();
            result.Should().NotBeEmpty().And.HaveCount(10);
            var index = 1;
            foreach (var item in result!)
            {
                item.Id.Should().Be(index);
                index++;
            }
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(5, 40)]
        [InlineData(10, 90)]
        [InlineData(51, 500)]
        [InlineData(100, 990)]

        public async Task Getting_all_products_will_return_correct_page_with_10_product_records_when_using_default_page_size(int pageNumber, int numberOfSkippedProducts)
        {
            var respond = await _httpClient.GetAsync($"api/v2/products?pagenumber={pageNumber}");

            respond.StatusCode.Should().Be(HttpStatusCode.OK);
            respond.Headers.Contains("X-Pagination").Should().BeTrue();

            var paginationMetadata = JsonConvert.DeserializeObject<PaginationMetadata>(respond.Headers.GetValues("X-Pagination").First());
            paginationMetadata!.PageNumber.Should().Be(pageNumber);
            paginationMetadata.TotalPages.Should().Be(100);
            paginationMetadata.HasNext.Should().Be(pageNumber < 100);
            paginationMetadata.HasPrevious.Should().Be(pageNumber > 1);

            var result = await respond.Content.ReadFromJsonAsync<IEnumerable<ProductRespond>>();
            result.Should().NotBeEmpty().And.HaveCount(10);

            var index = numberOfSkippedProducts + 1;
            foreach (var item in result!)
            {
                item.Id.Should().Be(index);
                index++;
            }
        }

        [Theory]
        [InlineData(10, 100)]
        [InlineData(25, 40)]
        [InlineData(100, 10)]
        public async Task Getting_all_products_will_return_correct_page_size_when_using_page_size_smaller_or_equal_then_100(int pageSize, int expectedTotalPages)
        {
            var respond = await _httpClient.GetAsync($"api/v2/products?pagesize={pageSize}");

            respond.StatusCode.Should().Be(HttpStatusCode.OK);
            respond.Headers.Contains("X-Pagination").Should().BeTrue();

            var paginationMetadata = JsonConvert.DeserializeObject<PaginationMetadata>(respond.Headers.GetValues("X-Pagination").First());
            paginationMetadata!.PageNumber.Should().Be(1);
            paginationMetadata.TotalPages.Should().Be(expectedTotalPages);
            paginationMetadata.HasNext.Should().Be(expectedTotalPages > 1);
            paginationMetadata.HasPrevious.Should().Be(expectedTotalPages < 2);

            var result = await respond.Content.ReadFromJsonAsync<IEnumerable<ProductRespond>>();
            result.Should().NotBeEmpty().And.HaveCount(pageSize);

            var index = 1;
            foreach (var item in result!)
            {
                item.Id.Should().Be(index);
                index++;
            }
        }

        [Theory]
        [InlineData(1000, 10)]
        [InlineData(500, 10)]
        public async Task Getting_all_products_will_return_max_page_size_100_when_using_page_size_greater_then_100(int pageSize, int expectedTotalPages)
        {
            var respond = await _httpClient.GetAsync($"api/v2/products?pagesize={pageSize}");

            respond.StatusCode.Should().Be(HttpStatusCode.OK);
            respond.Headers.Contains("X-Pagination").Should().BeTrue();

            var paginationMetadata = JsonConvert.DeserializeObject<PaginationMetadata>(respond.Headers.GetValues("X-Pagination").First());
            paginationMetadata!.PageNumber.Should().Be(1);
            paginationMetadata.TotalPages.Should().Be(expectedTotalPages);
            paginationMetadata.HasNext.Should().Be(expectedTotalPages > 1);
            paginationMetadata.HasPrevious.Should().Be(expectedTotalPages < 2);

            var result = await respond.Content.ReadFromJsonAsync<IEnumerable<ProductRespond>>();
            result.Should().NotBeEmpty().And.HaveCount(100);

            var index = 1;
            foreach (var item in result!)
            {
                item.Id.Should().Be(index);
                index++;
            }
        }

        [Theory]
        [InlineData(1, 10, 100, 0)]
        [InlineData(50, 10, 100, 490)]
        [InlineData(15, 70, 15, 980)]
        [InlineData(11, 100, 10, 1000)]
        public async Task Getting_all_products_when_using_page_number_and_page_size_together(int pageNumber, int pageSize, int expectedTotalPages, int numberOfSkippedProducts)
        {
            var respond = await _httpClient.GetAsync($"api/v2/products?pagenumber={pageNumber}&pagesize={pageSize}");

            respond.StatusCode.Should().Be(HttpStatusCode.OK);
            respond.Headers.Contains("X-Pagination").Should().BeTrue();

            var paginationMetadata = JsonConvert.DeserializeObject<PaginationMetadata>(respond.Headers.GetValues("X-Pagination").First());
            paginationMetadata!.PageNumber.Should().Be(pageNumber);
            paginationMetadata.TotalPages.Should().Be(expectedTotalPages);
            paginationMetadata.HasNext.Should().Be(pageNumber < expectedTotalPages);
            paginationMetadata.HasPrevious.Should().Be(pageNumber > 1);

            var result = await respond.Content.ReadFromJsonAsync<IEnumerable<ProductRespond>>();

            var index = numberOfSkippedProducts + 1;
            foreach (var item in result!)
            {
                item.Id.Should().Be(index);
                index++;
            }
        }
    }
}