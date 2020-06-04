using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Infrastructure.DataAccess.Repositories.Reading;
using WebFeatures.Infrastructure.Tests.Helpers.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Repositories.Reading
{
    [Collection("PostgreSqlDatabase")]
    public class ProductReadRepositoryTests
    {
        private readonly ProductReadRepository _repo;

        public ProductReadRepositoryTests(PostgreSqlDatabaseFixture db)
        {
            _repo = new ProductReadRepository(db.Connection);
        }

        [Fact]
        public async Task GetProductAsync_ShouldReturnProduct_IfProductExists()
        {
            // Arrange
            Guid productId = new Guid("4eee6f3a-3e71-4aaa-845b-d2b5529c5076");

            // Act
            ProductInfoDto product = await _repo.GetProductAsync(productId);

            // Assert
            product.ShouldNotBeNull();
            product.Id.ShouldBe(productId);
        }

        [Fact]
        public async Task GetProductAsync_ShouldReturnNull_IfProductDoesntExist()
        {
            // Arrange
            Guid productId = Guid.NewGuid();

            // Act
            ProductInfoDto product = await _repo.GetProductAsync(productId);

            // Assert
            product.ShouldBeNull();
        }

        [Fact]
        public async Task GetListAsync_ShouldReturnNonEmptyProductsCollection()
        {
            // Act
            IEnumerable<ProductListDto> list = await _repo.GetListAsync();

            // Assert
            list.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetCommentsAsync_ShouldReturnNonEmptyCollection_IfProductExists()
        {
            // Arrange
            Guid productId = new Guid("4eee6f3a-3e71-4aaa-845b-d2b5529c5076");

            // Act
            IEnumerable<ProductCommentInfoDto> comments = await _repo.GetCommentsAsync(productId);

            // Assert
            comments.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetCommentsAsync_ShouldReturnEmptyCollection_IsProductDoesntExist()
        {
            // Arrange
            Guid productId = Guid.NewGuid();

            // Act
            IEnumerable<ProductCommentInfoDto> comments = await _repo.GetCommentsAsync(productId);

            // Assert
            comments.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetReviewsAsync_ShouldReturnNonEmptyCollection_IfProductExists()
        {
            // Arrange
            Guid productId = new Guid("4eee6f3a-3e71-4aaa-845b-d2b5529c5076");

            // Act
            IEnumerable<ProductReviewInfoDto> reviews = await _repo.GetReviewsAsync(productId);

            // Assert
            reviews.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetReviewsAsync_ShouldReturnEmptyCollection_IfProductDoesntExist()
        {
            // Arrange
            Guid productId = Guid.NewGuid();

            // Act
            IEnumerable<ProductReviewInfoDto> reviews = await _repo.GetReviewsAsync(productId);

            // Assert
            reviews.ShouldBeEmpty();
        }
    }
}