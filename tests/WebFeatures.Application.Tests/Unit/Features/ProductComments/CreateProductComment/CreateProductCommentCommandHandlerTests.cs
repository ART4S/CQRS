using AutoMapper;
using Bogus;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductComments.CreateProductComment;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories;
using WebFeatures.Application.Interfaces.Events;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Application.Tests.Common.Stubs.Entities;
using WebFeatures.Application.Tests.Common.Stubs.Requests.ProductComments;
using WebFeatures.Domian.Entities.Products;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Features.ProductComments.CreateProductComment
{
    public class CreateProductCommentCommandHandlerTests
    {
        private readonly Mock<IWriteDbContext> _db;
        private readonly Mock<ICurrentUserService> _currentUser;
        private readonly Mock<IEventMediator> _events;
        private readonly Mock<IMapper> _mapper;

        public CreateProductCommentCommandHandlerTests()
        {
            _db = new Mock<IWriteDbContext>();
            _currentUser = new Mock<ICurrentUserService>();
            _events = new Mock<IEventMediator>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task HandleAsync_ShouldCreateComment()
        {
            // Arrange
            CreateProductCommentCommand request = new CreateProductCommentStub();

            ProductComment comment = new ProductCommentStub();

            _mapper.Setup(x => x.Map<ProductComment>(request)).Returns(comment);

            Guid userId = new Randomizer().Guid();

            _currentUser.Setup(x => x.UserId).Returns(userId);

            var commentsRepo = new Mock<IWriteRepository<ProductComment>>();

            _db.Setup(x => x.ProductComments).Returns(commentsRepo.Object);

            var handler = new CreateProductCommentCommandHandler(_db.Object, _currentUser.Object, _events.Object, _mapper.Object);

            // Act
            Guid commentId = await handler.HandleAsync(request, new CancellationToken());

            // Assert
            comment.Id.Should().Be(commentId);
            comment.AuthorId.Should().Be(userId);

            commentsRepo.Verify(x => x.CreateAsync(comment), Times.Once);
        }
    }
}