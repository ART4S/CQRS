using Bogus;
using FluentAssertions;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Requests.Commands;
using WebFeatures.Application.Interfaces.Files;
using WebFeatures.Application.Tests.Common.Base;
using WebFeatures.Domian.Entities.Products;
using Xunit;
using File = WebFeatures.Domian.Entities.File;
using ValidationException = WebFeatures.Application.Exceptions.ValidationException;

namespace WebFeatures.Application.Tests.Integration.Features.Products
{
    public class ProductCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task CreateProduct_CreatesProduct()
        {
            // Arrange
            var faker = new Faker();

            var mainPicture = new Mock<IFile>();

            mainPicture.Setup(x => x.Name).Returns("file.jpg");
            mainPicture.Setup(x => x.ContentType).Returns("application/json");
            mainPicture.Setup(x => x.OpenReadStream()).Returns(() => new MemoryStream());

            var request = new CreateProduct()
            {
                Name = faker.Lorem.Word(),
                MainPicture = mainPicture.Object,
                Description = faker.Lorem.Sentences(),
                BrandId = new Guid("f612a3d0-573a-47e5-9f6b-a941f99fb26f"),
                CategoryId = new Guid("03e9c4b2-7587-4640-b376-2437414fd610"),
                ManufacturerId = new Guid("278a79e9-5889-4953-a7c9-448c1e185600"),
                Pictures = new IFile[0]
            };

            // Act
            Guid productId = await Mediator.SendAsync(request);

            Product product = await DbContext.Products.GetAsync(productId);

            File mainPictureFile = await DbContext.Files.GetAsync(product.MainPictureId.Value);

            // Assert
            product.Id.Should().Be(productId);
            product.Name.Should().Be(request.Name);
            product.Price.Should().BeNull();
            product.MainPictureId.Should().Be(mainPictureFile.Id);
            product.Description.Should().Be(request.Description);
            product.BrandId.Should().Be(request.BrandId);
            product.CategoryId.Should().Be(request.CategoryId);
            product.ManufacturerId.Should().Be(request.ManufacturerId);

            mainPictureFile.Name.Should().Be(request.MainPicture.Name);
            mainPictureFile.ContentType.Should().Be(request.MainPicture.ContentType);
        }

        [Fact]
        public async Task CreateProduct_WhenInvalidProduct_Throws()
        {
            // Arrange
            var request = new CreateProduct();

            // Act
            Func<Task<Guid>> actual = () => Mediator.SendAsync(request);

            // Assert
            (await actual.Should().ThrowAsync<ValidationException>()).And.Error.Should().NotBeNull();
        }
    }
}