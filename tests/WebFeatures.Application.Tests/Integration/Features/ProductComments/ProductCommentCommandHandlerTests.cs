using FluentAssertions;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductComments.Requests.Commands;
using WebFeatures.Application.Tests.Common.Base;
using WebFeatures.Application.Tests.Common.Stubs.Requests.ProductComments;
using WebFeatures.Domian.Entities.Products;
using Xunit;
using ValidationException = WebFeatures.Application.Exceptions.ValidationException;

namespace WebFeatures.Application.Tests.Integration.Features.ProductComments
{
    public class ProductCommentCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task CreateProductComment_CreatesComment()
        {
            // Arrange
            CreateProductComment request = new CreateProductCommentStub();

            request.ProductId = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e");
            request.ParentCommentId = new Guid("c9502ede-2136-4202-8fe4-5e3d0006b0dc");

            // Act
            Guid adminId = await AuthenticateAdminAsync();

            Guid commentId = await Mediator.SendAsync(request);

            ProductComment comment = await DbContext.ProductComments.GetAsync(commentId);

            // Assert
            comment.Id.Should().Be(commentId);
            comment.CreateDate.Should().BeCloseTo(DateTime.UtcNow, 1000);
            comment.Body.Should().Be(request.Body);
            comment.ProductId.Should().Be(request.ProductId);
            comment.AuthorId.Should().Be(adminId);
            comment.ParentCommentId.Should().Be(request.ParentCommentId);
        }

        [Fact]
        public async Task CreateProductComment_WhenInvalidData_Throws()
        {
            // Arrange
            var request = new CreateProductComment();

            // Act
            Func<Task<Guid>> actual = () => Mediator.SendAsync(request);

            // Assert
            (await actual.Should().ThrowAsync<ValidationException>()).And.Error.Should().NotBeNull();
        }
    }
}