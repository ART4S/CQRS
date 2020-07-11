using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.GetProductList;
using WebFeatures.Application.Tests.Common.Base;
using Xunit;

namespace WebFeatures.Application.Tests.Integration.Features.Products.GetProductList
{
    public class GetProductListQueryTests : IntegrationTestBase
    {
        [Fact]
        public async Task ShouldReturnNotEmptyProductsCollection()
        {
            // Arrange
            var request = new GetProductListQuery();

            // Act
            IEnumerable<ProductListDto> products = await Mediator.SendAsync(request);

            // Assert
            products.Should().NotBeEmpty();
        }
    }
}