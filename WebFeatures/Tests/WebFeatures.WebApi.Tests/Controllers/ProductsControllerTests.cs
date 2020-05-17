using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.CreateProduct;
using WebFeatures.Application.Features.Products.EditProduct;
using WebFeatures.Application.Features.Products.GetProduct;
using WebFeatures.Application.Features.Products.GetProductComments;
using WebFeatures.Application.Features.Products.GetProductsList;
using WebFeatures.WebApi.Tests.Fixtures;
using Xunit;

namespace WebFeatures.WebApi.Tests.Controllers
{
    public class ProductsControllerTests : IClassFixture<TestServerFixture>
    {
        private readonly HttpClient _client;

        public ProductsControllerTests(TestServerFixture fixture)
        {
            _client = fixture.Server.CreateClient();
        }

        [Fact]
        public async Task GetProductById_ReturnsProduct()
        {
            // Arrange
            Guid productId = Guid.Parse("0f7b807f-3737-4997-9627-dbe5dc15310a");

            // Act
            HttpResponseMessage response = await _client.GetAsync($"api/products/{productId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var product = await response.ReadAsJsonAsync<ProductInfoDto>();

            Assert.Equal(productId, product.Id);
        }

        [Fact]
        public async Task GetProductsList_ReturnsProductsList()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("api/products/list");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var list = await response.ReadAsJsonAsync<List<ProductListDto>>();
            Assert.NotEmpty(list);

            Guid productId = Guid.Parse("0f7b807f-3737-4997-9627-dbe5dc15310a");
            Assert.Contains(list, x => x.Id == productId);
        }

        [Fact]
        public async Task GetProductReviews_ReturnsReviews()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync(
                "api/products/0f7b807f-3737-4997-9627-dbe5dc15310a/reviews");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var reviews = await response.ReadAsJsonAsync<List<ProductReviewInfoDto>>();
            Assert.NotEmpty(reviews);
        }

        [Fact]
        public async Task GetProductComments_ReturnsComments()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync(
                "api/products/0f7b807f-3737-4997-9627-dbe5dc15310a/comments");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var comments = await response.ReadAsJsonAsync<List<ProductCommentInfoDto>>();
            Assert.NotEmpty(comments);
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreatedProductId()
        {
            // Arrange
            var content = new MultipartFormDataContent
            {
                { new StringContent("test", Encoding.UTF8), nameof(CreateProduct.Name) },
                { new StringContent("7e6a526d-664e-4b8e-8f55-f78190aa9842", Encoding.UTF8), nameof(CreateProduct.BrandId) },
                { new StringContent("9ae5bdeb-9df9-4a2c-abd3-68afed9c6561", Encoding.UTF8), nameof(CreateProduct.CategoryId) },
                { new StringContent("100", Encoding.UTF8), nameof(CreateProduct.Price) },
                { new StringContent("6fde2362-9aaf-4bf7-886c-d0537fc8f7a6", Encoding.UTF8), nameof(CreateProduct.ManufacturerId) },
                { new StringContent("test description", Encoding.UTF8), nameof(CreateProduct.Description) }
            };

            // Act
            HttpResponseMessage response = await _client.PostAsync("api/products", content);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task EditProduct_ReturnsOk()
        {
            // Arrange
            string productId = "0f7b807f-3737-4997-9627-dbe5dc15310a";

            var content = new MultipartFormDataContent
            {
                { new StringContent(productId, Encoding.UTF8), nameof(EditProduct.Id) },
                { new StringContent("new name", Encoding.UTF8), nameof(EditProduct.Name) },
                { new StringContent("new description", Encoding.UTF8), nameof(EditProduct.Description) },
                { new StringContent("6fde2362-9aaf-4bf7-886c-d0537fc8f7a6", Encoding.UTF8), nameof(EditProduct.ManufacturerId) },
                { new StringContent("7e6a526d-664e-4b8e-8f55-f78190aa9842", Encoding.UTF8), nameof(EditProduct.BrandId) }
            };

            // Act
            HttpResponseMessage response = await _client.PutAsync($"api/products/{productId}", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
