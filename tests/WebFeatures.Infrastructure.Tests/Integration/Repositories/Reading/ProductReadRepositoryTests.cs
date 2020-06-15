using Shouldly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Infrastructure.DataAccess.Repositories.Reading;
using WebFeatures.Infrastructure.Tests.Common;
using WebFeatures.Infrastructure.Tests.Common.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Repositories.Reading
{
    public class ProductReadRepositoryTests : IntegrationTestBase
    {
        private readonly IDbConnection _connection;

        public ProductReadRepositoryTests(DatabaseFixture db) : base(db)
        {
            _connection = db.Connection;
        }

        private ProductReadRepository CreateDefaultRepository()
        {
            return new ProductReadRepository(_connection);
        }

        [Fact]
        public async Task GetProductAsync_ReturnsProduct_IfProductExists()
        {
            // Arrange
            ProductReadRepository repo = CreateDefaultRepository();
            Guid productId = new Guid("4eee6f3a-3e71-4aaa-845b-d2b5529c5076");

            // Act
            ProductInfoDto product = await repo.GetProductAsync(productId);

            // Assert
            product.ShouldNotBeNull();
            product.Id.ShouldBe(productId);
        }

        [Fact]
        public async Task GetProductAsync_ReturnsNull_IfProductDoesntExist()
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
        public async Task GetCommentsAsync_ReturnsNonEmptyCollection_IfProductExists()
        {
            // Arrange
            ProductReadRepository repo = CreateDefaultRepository();
            Guid productId = new Guid("4eee6f3a-3e71-4aaa-845b-d2b5529c5076");

            // Act
            IEnumerable<ProductCommentInfoDto> comments = await repo.GetCommentsAsync(productId);

            // Assert
            comments.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetCommentsAsync_ReturnsEmptyCollection_IsProductDoesntExist()
        {
            // Arrange
            ProductReadRepository repo = CreateDefaultRepository();
            Guid productId = Guid.NewGuid();

            // Act
            IEnumerable<ProductCommentInfoDto> comments = await repo.GetCommentsAsync(productId);

            // Assert
            comments.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetReviewsAsync_ReturnsNonEmptyCollection_IfProductExists()
        {
            // Arrange
            ProductReadRepository repo = CreateDefaultRepository();
            Guid productId = new Guid("4eee6f3a-3e71-4aaa-845b-d2b5529c5076");

            // Act
            IEnumerable<ProductReviewInfoDto> reviews = await repo.GetReviewsAsync(productId);

            // Assert
            reviews.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetReviewsAsync_ReturnsEmptyCollection_IfProductDoesntExist()
        {
            // Arrange
            ProductReadRepository repo = CreateDefaultRepository();
            Guid productId = Guid.NewGuid();

            // Act
            IEnumerable<ProductReviewInfoDto> reviews = await repo.GetReviewsAsync(productId);

            // Assert
            reviews.ShouldBeEmpty();
        }
    }
}