using FluentAssertions;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Features.Products.GetProduct;
using WebFeatures.Application.Tests.Common.Base;
using Xunit;

namespace WebFeatures.Application.Tests.Integration.Features.Products.GetProduct
{
    public class GetProductQueryTests : IntegrationTestBase
    {
        [Fact]
        public async Task ShouldReturnProduct()
        {
            // Arrange
            var request = new GetProductQuery() { Id = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e") };

            // Act
            ProductInfoDto product = await Mediator.SendAsync(request);

            // Assert
            product.Should().NotBeNull();
            product.Id.Should().Be(request.Id);
        }

        [Fact]
        public void ShouldThrow_WhenProductDoesntExist()
        {
            // Arrange
            var request = new GetProductQuery();

            // Act
            Func<Task<ProductInfoDto>> act = () => Mediator.SendAsync(request);

            // Assert
            act.Should().Throw<ValidationException>().And.Message.Should().NotBeNullOrEmpty();
        }
    }
}