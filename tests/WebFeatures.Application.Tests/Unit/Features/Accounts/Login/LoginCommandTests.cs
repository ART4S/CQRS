using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Features.Accounts.Login;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Application.Tests.Common.Stubs.Entities;
using WebFeatures.Application.Tests.Common.Stubs.Requests.Accounts;
using WebFeatures.Domian.Entities;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Features.Accounts.Login
{
    public class LoginCommandTests
    {
        private readonly Mock<IPasswordHasher> _hasher;
        private readonly Mock<IWriteDbContext> _context;
        private readonly Mock<ILogger<LoginCommand>> _logger;

        public LoginCommandTests()
        {
            _hasher = new Mock<IPasswordHasher>();
            _context = new Mock<IWriteDbContext>();
            _logger = new Mock<ILogger<LoginCommand>>();
        }

        [Fact]
        public async Task HandleAsync_ReturnsExistingUserId()
        {
            // Arrange
            LoginCommand request = new LoginStub();

            User user = new UserStub();

            var userRepo = new Mock<IUserWriteRepository>();

            userRepo.Setup(x => x.GetByEmailAsync(request.Email)).ReturnsAsync(user);

            _context.Setup(x => x.Users).Returns(userRepo.Object);

            _hasher.Setup(x => x.Verify(user.PasswordHash, request.Password)).Returns(true);

            var sut = new LoginCommandHandler(_context.Object, _hasher.Object, _logger.Object);

            // Act
            Guid userId = await sut.HandleAsync(request, new CancellationToken());

            // Assert
            userId.Should().Be(user.Id);
        }

        [Fact]
        public async Task HandleAsync_WhenUserDoesntExist_Throws()
        {
            // Arrange
            _context.Setup(x => x.Users).Returns(Mock.Of<IUserWriteRepository>());

            var sut = new LoginCommandHandler(_context.Object, _hasher.Object, _logger.Object);

            // Act
            Func<Task<Guid>> actual = () => sut.HandleAsync(new LoginCommand(), new CancellationToken());

            // Assert
            await actual.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task HandleAsync_WhenInvalidPassword_Throws()
        {
            // Arrange
            _context.Setup(x => x.Users).Returns(Mock.Of<IUserWriteRepository>());

            _hasher.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var sut = new LoginCommandHandler(_context.Object, _hasher.Object, _logger.Object);

            // Act
            Func<Task<Guid>> actual = () => sut.HandleAsync(new LoginCommand(), new CancellationToken());

            // Assert
            await actual.Should().ThrowAsync<ValidationException>();
        }
    }
}