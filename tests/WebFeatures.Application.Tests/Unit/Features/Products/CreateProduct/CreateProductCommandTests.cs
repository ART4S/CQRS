﻿using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.CreateProduct;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Application.Interfaces.Events;
using WebFeatures.Application.Interfaces.Files;
using WebFeatures.Application.Tests.Common.Stubs.Entities;
using WebFeatures.Application.Tests.Common.Stubs.Requests.Products;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Products;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Features.Products.CreateProduct
{
    public class CreateProductCommandTests
    {
        private readonly Mock<IWriteDbContext> _db;
        private readonly Mock<IEventMediator> _events;
        private readonly Mock<IFileReader> _fileReader;
        private readonly Mock<IMapper> _mapper;

        public CreateProductCommandTests()
        {
            _db = new Mock<IWriteDbContext>();
            _events = new Mock<IEventMediator>();
            _fileReader = new Mock<IFileReader>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task ShouldCreateProduct()
        {
            // Arrange
            var fileRepo = new Mock<IFileWriteRepository>();

            _db.Setup(x => x.Files).Returns(fileRepo.Object);

            CreateProductCommand request = new CreateProductStub();

            IFile mainPicture = Mock.Of<IFile>();

            request.MainPicture = mainPicture;

            File mainPictureFile = new FileStub();

            _fileReader.Setup(x => x.ReadAsync(
                mainPicture,
                It.IsAny<CancellationToken>())).ReturnsAsync(mainPictureFile);

            Product product = new ProductStub();

            _mapper.Setup(x => x.Map<Product>(request)).Returns(product);

            var productRepo = new Mock<IWriteRepository<Product>>();

            _db.Setup(x => x.Products).Returns(productRepo.Object);

            var handler = new CreateProductCommandHandler(_db.Object, _events.Object, _fileReader.Object, _mapper.Object);

            // Act
            Guid productId = await handler.HandleAsync(request, new CancellationToken());

            // Assert
            product.Id.Should().Be(productId);
            product.MainPictureId.Should().Be(mainPictureFile.Id);

            fileRepo.Verify(x => x.CreateAsync(mainPictureFile), Times.Once);
            productRepo.Verify(x => x.CreateAsync(product), Times.Once);
        }

        [Fact]
        public async Task ShouldCreatePictures()
        {
            // Arrange
            var fileRepo = new Mock<IFileWriteRepository>();

            _db.Setup(x => x.Files).Returns(fileRepo.Object);

            _fileReader.Setup(x => x.ReadAsync(
                    It.IsAny<IFile>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FileStub());

            Product product = new ProductStub();

            _mapper.Setup(x => x.Map<Product>(It.IsAny<CreateProductCommand>())).Returns(product);

            _db.Setup(x => x.Products).Returns(Mock.Of<IWriteRepository<Product>>());

            var productPictureRepo = new Mock<IWriteRepository<ProductPicture>>();

            _db.Setup(x => x.ProductPictures).Returns(productPictureRepo.Object);

            CreateProductCommand request = new CreateProductStub();

            IFile picture = Mock.Of<IFile>();

            request.Pictures = new[] { picture };

            File pictureFile = new FileStub();

            _fileReader.Setup(x => x.ReadAsync(
                    picture,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(pictureFile);

            var sut = new CreateProductCommandHandler(_db.Object, _events.Object, _fileReader.Object, _mapper.Object);

            // Act
            await sut.HandleAsync(request, new CancellationToken());

            // Assert
            fileRepo.Verify(x => x.CreateAsync(pictureFile), Times.Once);

            productPictureRepo.Verify(x => x.CreateAsync(
                It.Is<ProductPicture>(y =>
                    y.FileId == pictureFile.Id &&
                    y.ProductId == product.Id)), Times.Once);
        }
    }
}