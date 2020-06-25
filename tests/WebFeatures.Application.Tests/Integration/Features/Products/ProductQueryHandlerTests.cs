using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Features.Products.Requests.Queries;
using WebFeatures.Application.Tests.Common.Base;
using Xunit;

namespace WebFeatures.Application.Tests.Integration.Features.Products
{
    public class ProductQueryHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task GetProduct_ReturnsExistingProduct()
        {
            // Arrange
            var request = new GetProduct() { Id = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e") };

            // Act
            ProductInfoDto product = await Mediator.SendAsync(request);

            // Assert
            product.Should().NotBeNull();
            product.Id.Should().Be(request.Id);
        }

        [Fact]
        public async Task GetProduct_WhenProductDoesntExist_Throws()
        {
            // Arrange
            var request = new GetProduct() { Id = Guid.NewGuid() };

            // Act
            Func<Task<ProductInfoDto>> actual = () => Mediator.SendAsync(request);

            // Assert
            (await actual.Should().ThrowAsync<ValidationException>()).And.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetProductList_ReturnsNotEmptyProductsCollection()
        {
            // Arrange
            var request = new GetProductList();

            // Act
            IEnumerable<ProductListDto> products = await Mediator.SendAsync(request);

            // Assert
            products.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetProductComments_ReturnsNotEmptyProductCommentsCollection()
        {
            // Arrange
            var request = new GetProductComments() { Id = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e") };

            // Act
            IEnumerable<ProductCommentInfoDto> comments = await Mediator.SendAsync(request);

            // Assert
            comments.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetProductReviews_ReturnsNotEmptyProductReviewsColection()
        {
            // Arrange
            var request = new GetProductReviews() { Id = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e") };

            // Act
            IEnumerable<ProductReviewInfoDto> reviews = await Mediator.SendAsync(request);

            // Assert
            reviews.Should().NotBeEmpty();
        }
    }
}