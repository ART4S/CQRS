using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.GetProduct;
using WebFeatures.Application.Features.Products.GetProductComments;
using WebFeatures.Application.Features.Products.GetProductsList;
using WebFeatures.Application.Features.Products.GetRatingsSummary;
using WebFeatures.Application.Features.Products.GetReviews;
using Xunit;

namespace WebFeatures.WebApi.Tests.Controllers
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task GetProductById_ReturnsProduct()
        {
            // Arrange
            HttpClient client = await HttpClientFactory.Create();
            Guid productId = Guid.Parse("0f7b807f-3737-4997-9627-dbe5dc15310a");

            // Act
            string url = $"api/products/{productId}";
            HttpResponseMessage response = await client.GetAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var product = await response.ReadAsJsonAsync<ProductInfoDto>();

            Assert.Equal(productId, product.Id);
        }

        [Fact]
        public async Task GetProductsList_ReturnsProductsList()
        {
            // Arrange
            HttpClient client = await HttpClientFactory.Create();
            Guid productId = Guid.Parse("0f7b807f-3737-4997-9627-dbe5dc15310a");

            // Act
            string url = "api/products/list";
            HttpResponseMessage response = await client.GetAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var list = await response.ReadAsJsonAsync<List<ProductListDto>>();

            Assert.NotEmpty(list);
            Assert.Contains(list, x => x.Id == productId);
        }

        [Fact]
        public async Task GetProductReviews_ReturnsReviews()
        {
            // Arrange
            HttpClient client = await HttpClientFactory.Create();
            Guid productId = Guid.Parse("0f7b807f-3737-4997-9627-dbe5dc15310a");

            // Act
            string url = $"api/products/{productId}/reviews";
            HttpResponseMessage response = await client.GetAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var reviews = await response.ReadAsJsonAsync<List<ReviewInfoDto>>();
            Assert.NotEmpty(reviews);
        }

        [Fact]
        public async Task GetProductComments_ReturnsComments()
        {
            // Arrange
            HttpClient client = await HttpClientFactory.Create();
            Guid productId = Guid.Parse("0f7b807f-3737-4997-9627-dbe5dc15310a");

            // Act
            string url = $"api/products/{productId}/comments";
            HttpResponseMessage response = await client.GetAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var comments = await response.ReadAsJsonAsync<List<CommentInfoDto>>();
            Assert.NotEmpty(comments);
        }

        [Fact]
        public async Task GetRatingsSummary_ReturnsSummary()
        {
            // Arrange
            HttpClient client = await HttpClientFactory.Create();
            Guid productId = Guid.Parse("0f7b807f-3737-4997-9627-dbe5dc15310a");

            // Act
            string url = $"api/products/{productId}/getRatingsSummary";
            HttpResponseMessage response = await client.GetAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var summary = await response.ReadAsJsonAsync<RatingsSummaryDto>();
            Assert.NotNull(summary);
        }
    }
}
