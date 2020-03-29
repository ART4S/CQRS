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
            string url = $"api/products/{productId}";

            // Act
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
            string url = "api/products/list";

            // Act
            HttpResponseMessage response = await client.GetAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var list = await response.ReadAsJsonAsync<List<ProductListDto>>();
            Assert.NotEmpty(list);

            Guid productId = Guid.Parse("0f7b807f-3737-4997-9627-dbe5dc15310a");
            Assert.Contains(list, x => x.Id == productId);
        }

        [Fact]
        public async Task GetProductReviews_ReturnsReviews()
        {
            // Arrange
            HttpClient client = await HttpClientFactory.Create();
            string url = "api/products/0f7b807f-3737-4997-9627-dbe5dc15310a/reviews";

            // Act
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
            string url = "api/products/0f7b807f-3737-4997-9627-dbe5dc15310a/comments";

            // Act
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
            string url = $"api/products/0f7b807f-3737-4997-9627-dbe5dc15310a/getRatingsSummary";

            // Act
            HttpResponseMessage response = await client.GetAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var summary = await response.ReadAsJsonAsync<RatingsSummaryDto>();
            Assert.NotNull(summary);
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreatedProductId()
        {
            // Arrange
            HttpClient client = await HttpClientFactory.Create();
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("test", Encoding.UTF8), nameof(CreateProduct.Name));
            content.Add(new StringContent("7e6a526d-664e-4b8e-8f55-f78190aa9842", Encoding.UTF8), nameof(CreateProduct.BrandId));
            content.Add(new StringContent("9ae5bdeb-9df9-4a2c-abd3-68afed9c6561", Encoding.UTF8), nameof(CreateProduct.CategoryId));
            content.Add(new StringContent("100", Encoding.UTF8), nameof(CreateProduct.Price));
            content.Add(new StringContent("6fde2362-9aaf-4bf7-886c-d0537fc8f7a6", Encoding.UTF8), nameof(CreateProduct.ManufacturerId));
            content.Add(new StringContent("test description", Encoding.UTF8), nameof(CreateProduct.Description));

            // Act
            string url = "api/products";
            HttpResponseMessage response = await client.PostAsync(url, content);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task EditProduct_ReturnsOK()
        {
            // Arrange
            HttpClient client = await HttpClientFactory.Create();
            string productId = "0f7b807f-3737-4997-9627-dbe5dc15310a";
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(productId, Encoding.UTF8), nameof(EditProduct.Id));
            content.Add(new StringContent("new name", Encoding.UTF8), nameof(EditProduct.Name));
            content.Add(new StringContent("new description", Encoding.UTF8), nameof(EditProduct.Description));
            content.Add(new StringContent("6fde2362-9aaf-4bf7-886c-d0537fc8f7a6", Encoding.UTF8), nameof(EditProduct.ManufacturerId));
            content.Add(new StringContent("7e6a526d-664e-4b8e-8f55-f78190aa9842", Encoding.UTF8), nameof(EditProduct.BrandId));

            // Act
            string url = $"api/products/{productId}";
            HttpResponseMessage response = await client.PutAsync(url, content);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
