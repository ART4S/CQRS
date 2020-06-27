using FluentAssertions;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.DeleteProduct;
using WebFeatures.Application.Tests.Common.Base;
using WebFeatures.Domian.Entities.Products;
using Xunit;

namespace WebFeatures.Application.Tests.Integration.Features.Products.DeleteProduct
{
    public class DeleteProductCommandTests : IntegrationTestBase
    {
        [Fact]
        public async Task HandleAsync_DeletesExistingProduct()
        {
            // Arrange
            var request = new DeleteProductCommand() { Id = new Guid("f321a9fa-fc44-47e9-9739-bb4d57724f3e") };

            // Act
            await Mediator.SendAsync(request);

            Product product = await DbContext.Products.GetAsync(request.Id);

            // Assert
            product.Should().BeNull();
        }
    }
}