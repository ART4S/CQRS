using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.GetProductComments;
using WebFeatures.Application.Tests.Common.Base;
using Xunit;

namespace WebFeatures.Application.Tests.Integration.Features.Products.GetProductComments
{
    public class GetProductCommentsQueryTests : IntegrationTestBase
    {
        [Fact]
        public async Task HandleAsync_ReturnsNotEmptyProductCommentsCollection()
        {
            // Arrange
            var request = new GetProductCommentsQuery() { Id = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e") };

            // Act
            IEnumerable<ProductCommentInfoDto> comments = await Mediator.SendAsync(request);

            // Assert
            comments.Should().NotBeEmpty();
        }
    }
}