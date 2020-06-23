using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductReviews.Handlers;
using WebFeatures.Application.Features.ProductReviews.Requests.Commands;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories;
using WebFeatures.Application.Interfaces.Events;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Application.Tests.Common.Stubs.Entities;
using WebFeatures.Application.Tests.Common.Stubs.Requests.ProductReviews;
using WebFeatures.Domian.Entities.Products;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Features.ProductReviews
{
    public class ProductReviewCommandHandlerTests
    {
        private readonly Mock<IWriteDbContext> _db;
        private readonly Mock<ICurrentUserService> _currentUser;
        private readonly Mock<IEventMediator> _events;
        private readonly Mock<IMapper> _mapper;

        public ProductReviewCommandHandlerTests()
        {
            _db = new Mock<IWriteDbContext>();
            _currentUser = new Mock<ICurrentUserService>();
            _events = new Mock<IEventMediator>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task CreateProductReview_ShouldCreateReview()
        {
            // Arrange
            CreateProductReview request = new CreateProductReviewStub();

            ProductReview review = new ProductReviewStub();

            _mapper.Setup(x => x.Map<ProductReview>(request)).Returns(review);

            var reviewRepo = new Mock<IWriteRepository<ProductReview>>();

            _db.Setup(x => x.ProductReviews).Returns(reviewRepo.Object);

            var handler = new ProductReviewCommandHandler(_db.Object, _events.Object, _currentUser.Object, _mapper.Object);

            // Act
            Guid reviewId = await handler.HandleAsync(request, new CancellationToken());

            // Assert
            review.Id.Should().Be(reviewId);

            reviewRepo.Verify(x => x.CreateAsync(review), Times.Once);
        }
    }
}