using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.GetProduct;
using WebFeatures.Application.Features.Products.GetProductComments;
using WebFeatures.Application.Features.Products.GetProductList;
using WebFeatures.Application.Features.Products.GetProductReviews;
using WebFeatures.Infrastructure.DataAccess.QueryExecutors;
using WebFeatures.Infrastructure.DataAccess.Repositories.Reading;
using WebFeatures.Infrastructure.Tests.Common.Base;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Repositories.Reading
{
    public class ProductReadRepositoryTests : IntegrationTestBase
    {
        private ProductReadRepository CreateDefaultRepository()
        {
            return new ProductReadRepository(Database.Connection, new DapperDbExecutor());
        }

        [Fact]
        public async Task GetProductAsync_WhenProductExists_ReturnsProduct()
        {
            // Arrange
            ProductReadRepository sut = CreateDefaultRepository();

            Guid productId = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e");

            // Act
            ProductInfoDto product = await sut.GetProductAsync(productId);

            // Assert
            product.Should().NotBeNull();
            product.Id.Should().Be(productId);
        }

        [Fact]
        public async Task GetProductAsync_WhenProductDoesntExist_ReturnsNull()
        {
            // Arrange
            ProductReadRepository sut = CreateDefaultRepository();

            // Act
            ProductInfoDto product = await sut.GetProductAsync(Guid.NewGuid());

            // Assert
            product.Should().BeNull();
        }

        [Fact]
        public async Task GetListAsync_ReturnsNonEmptyProductsCollection()
        {
            // Arrange
            ProductReadRepository sut = CreateDefaultRepository();

            // Act
            IEnumerable<ProductListDto> list = await sut.GetListAsync();

            // Assert
            list.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetCommentsAsync_ReturnsNonEmptyCollection()
        {
            // Arrange
            ProductReadRepository sut = CreateDefaultRepository();

            Guid productId = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e");

            // Act
            IEnumerable<ProductCommentInfoDto> comments = await sut.GetCommentsAsync(productId);

            // Assert
            comments.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetCommentsAsync_WhenProductDoesntExist_ReturnsEmptyCollection()
        {
            // Arrange
            ProductReadRepository sut = CreateDefaultRepository();

            Guid productId = new Guid("0cacb23f-da1f-44c6-80e0-5204d99e06b9");

            // Act
            IEnumerable<ProductCommentInfoDto> comments = await sut.GetCommentsAsync(productId);

            // Assert
            comments.Should().BeEmpty();
        }

        [Fact]
        public async Task GetReviewsAsync_WhenProductExists_ReturnsNonEmptyCollection()
        {
            // Arrange
            ProductReadRepository sut = CreateDefaultRepository();

            Guid productId = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e");

            // Act
            IEnumerable<ProductReviewInfoDto> reviews = await sut.GetReviewsAsync(productId);

            // Assert
            reviews.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetReviewsAsync_WhenProductDoesntExist_ReturnsEmptyCollection()
        {
            // Arrange
            ProductReadRepository sut = CreateDefaultRepository();

            Guid productId = new Guid("0cacb23f-da1f-44c6-80e0-5204d99e06b9");

            // Act
            IEnumerable<ProductReviewInfoDto> reviews = await sut.GetReviewsAsync(productId);

            // Assert
            reviews.Should().BeEmpty();
        }
    }
}