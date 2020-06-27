using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.GetProductReviews;
using WebFeatures.Application.Tests.Common.Base;
using Xunit;

namespace WebFeatures.Application.Tests.Integration.Features.Products.GetProductReviews
{
    public class GetProductReviewsQueryTests : IntegrationTestBase
    {
        [Fact]
        public async Task HandleAsync_ReturnsNotEmptyProductReviewsColection()
        {
            // Arrange
            var request = new GetProductReviewsQuery() { Id = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e") };

            // Act
            IEnumerable<ProductReviewInfoDto> reviews = await Mediator.SendAsync(request);

            // Assert
            reviews.Should().NotBeEmpty();
        }
    }
}