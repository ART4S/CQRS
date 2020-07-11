using Bogus;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.CreateProduct;
using WebFeatures.Application.Interfaces.Files;
using WebFeatures.Application.Tests.Common.Base;
using WebFeatures.Application.Tests.Common.Stubs.Files;
using WebFeatures.Domian.Entities.Products;
using Xunit;
using ValidationException = WebFeatures.Application.Exceptions.ValidationException;

namespace WebFeatures.Application.Tests.Integration.Features.Products.CreateProduct
{
    public class CreateProductCommandTests : IntegrationTestBase
    {
        [Fact]
        public async Task ShouldCreateProduct()
        {
            // Arrange
            var faker = new Faker();

            var request = new CreateProductCommand()
            {
                Name = faker.Lorem.Word(),
                MainPicture = new CustomFile().WithExtension("jpg"),
                Description = faker.Lorem.Sentences(),
                BrandId = new Guid("f612a3d0-573a-47e5-9f6b-a941f99fb26f"),
                CategoryId = new Guid("03e9c4b2-7587-4640-b376-2437414fd610"),
                ManufacturerId = new Guid("278a79e9-5889-4953-a7c9-448c1e185600"),
                Pictures = new IFile[] { new CustomFile().WithExtension("jpg") }
            };

            // Act
            Guid productId = await Mediator.SendAsync(request);

            Product product = await DbContext.Products.GetAsync(productId);

            // Assert
            product.Should().NotBeNull();
            product.Id.Should().Be(productId);
            product.Name.Should().Be(request.Name);
            product.Price.Should().BeNull();
            product.MainPictureId.Should().NotBeNull();
            product.Description.Should().Be(request.Description);
            product.BrandId.Should().Be(request.BrandId);
            product.CategoryId.Should().Be(request.CategoryId);
            product.ManufacturerId.Should().Be(request.ManufacturerId);
        }

        [Fact]
        public void ShouldThrow_WhenInvalidProduct()
        {
            // Arrange
            var request = new CreateProductCommand();

            // Act
            Func<Task<Guid>> act = () => Mediator.SendAsync(request);

            // Assert
            act.Should().Throw<ValidationException>().And.Error.Should().NotBeNull();
        }
    }
}