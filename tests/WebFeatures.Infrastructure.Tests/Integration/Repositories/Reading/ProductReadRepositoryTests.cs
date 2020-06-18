using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Infrastructure.DataAccess.QueryExecutors;
using WebFeatures.Infrastructure.DataAccess.Repositories.Reading;
using WebFeatures.Infrastructure.Tests.Common.Base;
using WebFeatures.Infrastructure.Tests.Common.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Repositories.Reading
{
    public class ProductReadRepositoryTests : IntegrationTestBase
    {
        public ProductReadRepositoryTests(DatabaseFixture database) : base(database)
        {
        }

        private ProductReadRepository CreateDefaultRepository()
        {
            return new ProductReadRepository(Database.Connection, new DapperDbExecutor());
        }

        [Fact]
        public async Task GetProductAsync_WhenProductExists_ReturnsProduct()
        {
            // Arrange
            ProductReadRepository repo = CreateDefaultRepository();
            Guid productId = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e");

            // Act
            ProductInfoDto product = await repo.GetProductAsync(productId);

            // Assert
            product.ShouldNotBeNull();
            product.Id.ShouldBe(productId);
        }

        [Fact]
        public async Task GetProductAsync_WhenProductDoesntExist_ReturnsNull()
        {
            // Arrange
            ProductReadRepository repo = CreateDefaultRepository();
            Guid productId = Guid.NewGuid();

            // Act
            ProductInfoDto product = await repo.GetProductAsync(productId);

            // Assert
            product.ShouldBeNull();
        }

        [Fact]
        public async Task GetListAsync_ReturnsNonEmptyProductsCollection()
        {
            // Arrange
            ProductReadRepository repo = CreateDefaultRepository();

            // Act
            IEnumerable<ProductListDto> list = await repo.GetListAsync();

            // Assert
            list.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetCommentsAsync_ReturnsNonEmptyCollection()
        {
            // Arrange
            ProductReadRepository repo = CreateDefaultRepository();
            Guid productId = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e");

            // Act
            IEnumerable<ProductCommentInfoDto> comments = await repo.GetCommentsAsync(productId);

            // Assert
            comments.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetCommentsAsync_WhenProductDoesntExist_ReturnsEmptyCollection()
        {
            // Arrange
            ProductReadRepository repo = CreateDefaultRepository();
            Guid productId = new Guid("0cacb23f-da1f-44c6-80e0-5204d99e06b9");

            // Act
            IEnumerable<ProductCommentInfoDto> comments = await repo.GetCommentsAsync(productId);

            // Assert
            comments.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetReviewsAsync_WhenProductExists_ReturnsNonEmptyCollection()
        {
            // Arrange
            ProductReadRepository repo = CreateDefaultRepository();
            Guid productId = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e");

            // Act
            IEnumerable<ProductReviewInfoDto> reviews = await repo.GetReviewsAsync(productId);

            // Assert
            reviews.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetReviewsAsync_WhenProductDoesntExist_ReturnsEmptyCollection()
        {
            // Arrange
            ProductReadRepository repo = CreateDefaultRepository();
            Guid productId = new Guid("0cacb23f-da1f-44c6-80e0-5204d99e06b9");

            // Act
            IEnumerable<ProductReviewInfoDto> reviews = await repo.GetReviewsAsync(productId);

            // Assert
            reviews.ShouldBeEmpty();
        }
    }
}