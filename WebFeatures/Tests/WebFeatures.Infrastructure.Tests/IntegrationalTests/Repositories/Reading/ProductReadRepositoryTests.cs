using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Infrastructure.DataAccess.Repositories.Reading;
using WebFeatures.Infrastructure.Tests.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.IntegrationalTests.Repositories.Reading
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
        public async Task GetListAsync_ReturnsNonEmptyProductsCollection()
        {
            // Act
            IEnumerable<ProductListDto> list = await _repo.GetListAsync();

            // Assert
            list.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetProductAsync_ReturnsExistigProduct()
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
        public async Task GetCommentsAsync_ReturnsNonEmptyCommentsCollection()
        {
            // Arrange
            Guid productId = new Guid("4eee6f3a-3e71-4aaa-845b-d2b5529c5076");

            // Act
            IEnumerable<ProductCommentInfoDto> comments = await _repo.GetCommentsAsync(productId);

            // Assert
            comments.ShouldNotBeEmpty();
        }


        [Fact]
        public async Task GetReviewsAsync_ReturnsNonEmptyReviewsCollection()
        {
            // Arrange
            Guid productId = new Guid("4eee6f3a-3e71-4aaa-845b-d2b5529c5076");

            // Act
            IEnumerable<ProductReviewInfoDto> reviews = await _repo.GetReviewsAsync(productId);

            // Assert
            reviews.ShouldNotBeEmpty();
        }
    }
}