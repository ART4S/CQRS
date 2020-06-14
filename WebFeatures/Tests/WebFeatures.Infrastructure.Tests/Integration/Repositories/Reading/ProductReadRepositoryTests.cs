using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Infrastructure.DataAccess.Repositories.Reading;
using WebFeatures.Infrastructure.Tests.Common.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Repositories.Reading
{
    [Collection("PostgreSqlDatabase")]
    public class ProductReadRepositoryTests
    {
        private readonly PostgreSqlDatabaseFixture _db;

        public ProductReadRepositoryTests(PostgreSqlDatabaseFixture db)
        {
            _db = db;
        }

        private ProductReadRepository CreateDefaultRepository()
        {
            return new ProductReadRepository(_db.Connection);
        }

        [Fact]
        public async Task GetProductAsync_ShouldReturnProduct_IfProductExists()
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
        public async Task GetProductAsync_ShouldReturnNull_IfProductDoesntExist()
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
        public async Task GetListAsync_ShouldReturnNonEmptyProductsCollection()
        {
            // Arrange
            ProductReadRepository repo = CreateDefaultRepository();

            // Act
            IEnumerable<ProductListDto> list = await repo.GetListAsync();

            // Assert
            list.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetCommentsAsync_ShouldReturnNonEmptyCollection_IfProductExists()
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
        public async Task GetCommentsAsync_ShouldReturnEmptyCollection_IsProductDoesntExist()
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
        public async Task GetReviewsAsync_ShouldReturnNonEmptyCollection_IfProductExists()
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
        public async Task GetReviewsAsync_ShouldReturnEmptyCollection_IfProductDoesntExist()
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