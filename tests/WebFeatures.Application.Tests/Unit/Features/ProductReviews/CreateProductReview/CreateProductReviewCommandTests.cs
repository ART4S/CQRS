using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductReviews.CreateProductReview;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Application.Interfaces.Events;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Application.Tests.Common.Stubs.Entities;
using WebFeatures.Application.Tests.Common.Stubs.Requests.ProductReviews;
using WebFeatures.Domian.Entities.Products;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Features.ProductReviews.CreateProductReview
{
    public class CreateProductReviewCommandTests
    {
        private readonly Mock<IWriteDbContext> _db;
        private readonly Mock<ICurrentUserService> _currentUser;
        private readonly Mock<IEventMediator> _events;
        private readonly Mock<IMapper> _mapper;

        public CreateProductReviewCommandTests()
        {
            _db = new Mock<IWriteDbContext>();
            _currentUser = new Mock<ICurrentUserService>();
            _events = new Mock<IEventMediator>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task ShouldCreateReview()
        {
            // Arrange
            CreateProductReviewCommand request = new CreateProductReviewStub();

            ProductReview review = new ProductReviewStub();

            _mapper.Setup(x => x.Map<ProductReview>(request)).Returns(review);

            var reviewRepo = new Mock<IWriteRepository<ProductReview>>();

            _db.Setup(x => x.ProductReviews).Returns(reviewRepo.Object);

            var sut = new CreateProductReviewCommandHandler(_db.Object, _events.Object, _currentUser.Object, _mapper.Object);

            // Act
            Guid reviewId = await sut.HandleAsync(request, new CancellationToken());

            // Assert
            review.Id.Should().Be(reviewId);

            reviewRepo.Verify(x => x.CreateAsync(review), Times.Once);
        }
    }
}