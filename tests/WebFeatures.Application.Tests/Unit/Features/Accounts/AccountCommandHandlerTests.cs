using Bogus;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Accounts.Handlers;
using WebFeatures.Application.Features.Accounts.Requests.Commands;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Application.Tests.Common.Stubs.Entities;
using WebFeatures.Application.Tests.Common.Stubs.Requests.Accounts;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Permissions;
using Xunit;
using ILoggerFactory = WebFeatures.Application.Interfaces.Logging.ILoggerFactory;
using ValidationException = WebFeatures.Application.Exceptions.ValidationException;

namespace WebFeatures.Application.Tests.Unit.Features.Accounts
{
    public class AccountCommandHandlerTests
    {
        private readonly Mock<IPasswordHasher> _hasher;
        private readonly Mock<IWriteDbContext> _context;
        private readonly Mock<ILoggerFactory> _loggerFactory;

        public AccountCommandHandlerTests()
        {
            _hasher = new Mock<IPasswordHasher>();
            _context = new Mock<IWriteDbContext>();
            _loggerFactory = new Mock<ILoggerFactory>();
        }

        [Fact]
        public async Task Register_CreatesNewUser()
        {
            // Arrange
            Register request = new RegisterStub();

            string hash = new Randomizer().Utf16String();

            _hasher.Setup(x => x.ComputeHash(request.Password)).Returns(hash);

            var userRepo = new Mock<IUserWriteRepository>();

            Guid expectedUserId = new Faker().Random.Guid();

            userRepo.Setup(x => x.CreateAsync(
                    It.Is<User>(x => x.Email == request.Email && x.Name == request.Name && x.PasswordHash == hash)))
                .Callback<User>(x => x.Id = expectedUserId);

            _context.Setup(x => x.Users).Returns(userRepo.Object);

            var roleRepo = new Mock<IRoleWriteRepository>();

            roleRepo.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(new RoleStub());

            _context.Setup(x => x.Roles).Returns(roleRepo.Object);

            _context.Setup(x => x.UserRoles).Returns(Mock.Of<IWriteRepository<UserRole>>());

            _loggerFactory.Setup(x => x.CreateLogger<Register>()).Returns(Mock.Of<ILogger<Register>>());

            var handler = new AccountCommandHandler(_context.Object, _hasher.Object, _loggerFactory.Object);

            // Act
            Guid actualUserId = await handler.HandleAsync(request, new CancellationToken());

            // Assert
            actualUserId.Should().Be(expectedUserId);
        }

        [Fact]
        public async Task Register_WhenRoleForUserIsMissing_Throws()
        {
            // Arrange
            _context.Setup(x => x.Roles).Returns(Mock.Of<IRoleWriteRepository>());
            _context.Setup(x => x.Users).Returns(Mock.Of<IUserWriteRepository>());

            var handler = new AccountCommandHandler(_context.Object, _hasher.Object, _loggerFactory.Object);

            // Act
            Func<Task> actual = () => handler.HandleAsync(new Register(), new CancellationToken());

            // Assert
            await actual.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task Login_ReturnsExistingUserId()
        {
            // Arrange
            Login request = new LoginStub();

            User user = new UserStub();

            var userRepo = new Mock<IUserWriteRepository>();

            userRepo.Setup(x => x.GetByEmailAsync(request.Email)).ReturnsAsync(user);

            _context.Setup(x => x.Users).Returns(userRepo.Object);

            _hasher.Setup(x => x.Verify(user.PasswordHash, request.Password)).Returns(true);

            _loggerFactory.Setup(x => x.CreateLogger<Login>()).Returns(Mock.Of<ILogger<Login>>());

            var handler = new AccountCommandHandler(_context.Object, _hasher.Object, _loggerFactory.Object);

            // Act
            Guid userId = await handler.HandleAsync(request, new CancellationToken());

            // Assert
            userId.Should().Be(user.Id);
        }

        [Fact]
        public async Task Login_WhenUserDoesntExist_Throws()
        {
            // Arrange
            _context.Setup(x => x.Users).Returns(Mock.Of<IUserWriteRepository>());

            var handler = new AccountCommandHandler(_context.Object, _hasher.Object, _loggerFactory.Object);

            // Act
            Func<Task<Guid>> actual = () => handler.HandleAsync(new Login(), new CancellationToken());

            // Assert
            await actual.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Login_WhenInvalidPassword_Throws()
        {
            // Arrange
            _context.Setup(x => x.Users).Returns(Mock.Of<IUserWriteRepository>());

            _hasher.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var handler = new AccountCommandHandler(_context.Object, _hasher.Object, _loggerFactory.Object);

            // Act
            Func<Task<Guid>> actual = () => handler.HandleAsync(new Login(), new CancellationToken());

            // Assert
            await actual.Should().ThrowAsync<ValidationException>();
        }
    }
}