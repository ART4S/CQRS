using Bogus;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductComments.CreateProductComment;
using WebFeatures.Application.Tests.Common.Base;
using WebFeatures.Domian.Entities.Products;
using Xunit;
using ValidationException = WebFeatures.Application.Exceptions.ValidationException;

namespace WebFeatures.Application.Tests.Integration.Features.ProductComments.CreateProductComment
{
    public class CreateProductCommentCommandTests : IntegrationTestBase
    {
        [Fact]
        public async Task ShouldCreateComment()
        {
            // Arrange
            var request = new CreateProductCommentCommand()
            {
                ProductId = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e"),
                ParentCommentId = new Guid("c9502ede-2136-4202-8fe4-5e3d0006b0dc"),
                Body = new Faker().Lorem.Text()
            };

            // Act
            Guid adminId = await AuthenticateAdminAsync();

            Guid commentId = await Mediator.SendAsync(request);

            ProductComment comment = await DbContext.ProductComments.GetAsync(commentId);

            // Assert
            comment.Should().NotBeNull();
            comment.Id.Should().Be(commentId);
            comment.CreateDate.Should().BeCloseTo(DateTime.UtcNow, 1000);
            comment.Body.Should().Be(request.Body);
            comment.ProductId.Should().Be(request.ProductId);
            comment.AuthorId.Should().Be(adminId);
            comment.ParentCommentId.Should().Be(request.ParentCommentId);
        }

        [Fact]
        public void ShouldThrow_WhenInvalidComment()
        {
            // Arrange
            var request = new CreateProductCommentCommand();

            // Act
            Func<Task<Guid>> act = () => Mediator.SendAsync(request);

            // Assert
            act.Should().Throw<ValidationException>().And.Error.Should().NotBeNull();
        }
    }
}