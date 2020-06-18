using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Features.Accounts.Handlers;
using WebFeatures.Application.Features.Accounts.Requests.Commands;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Permissions;
using Xunit;
using ILoggerFactory = WebFeatures.Application.Interfaces.Logging.ILoggerFactory;

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
        public async Task Register_ReturnsNewUserId()
        {
            // Arrange
            var request = new Register()
            {
                Email = "email",
                Name = "name",
                Password = "password"
            };

            var role = new Role()
            {
                Name = "name"
            };

            string hash = "hash";

            Guid expectedUserId = Guid.NewGuid();

            _hasher.Setup(x => x.ComputeHash(request.Password)).Returns(hash);

            var userRepo = new Mock<IUserWriteRepository>();

            userRepo.Setup(x => x.CreateAsync(
                    It.Is<User>(x => x.Email == request.Email && x.Name == request.Name && x.PasswordHash == hash)))
                .Callback<User>(x => x.Id = expectedUserId);

            var roleRepo = new Mock<IRoleWriteRepository>();

            roleRepo.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(role);

            var userRoleRepo = new Mock<IWriteRepository<UserRole>>();

            _context.Setup(x => x.Users).Returns(userRepo.Object);
            _context.Setup(x => x.Roles).Returns(roleRepo.Object);
            _context.Setup(x => x.UserRoles).Returns(userRoleRepo.Object);

            var logger = new Mock<ILogger<Register>>();

            _loggerFactory.Setup(x => x.CreateLogger<Register>()).Returns(logger.Object);

            var handler = new AccountCommandHandler(_context.Object, _hasher.Object, _loggerFactory.Object);

            // Act
            Guid actualUserId = await handler.HandleAsync(request, new CancellationToken());

            // Assert
            actualUserId.ShouldBe(expectedUserId);
        }

        [Fact]
        public async Task Register_WhenRoleForUserIsMissing_Throws()
        {
            // Arrange
            var userRepo = new Mock<IUserWriteRepository>();
            var roleRepo = new Mock<IRoleWriteRepository>();

            _context.Setup(x => x.Users).Returns(userRepo.Object);
            _context.Setup(x => x.Roles).Returns(roleRepo.Object);

            var handler = new AccountCommandHandler(_context.Object, _hasher.Object, _loggerFactory.Object);

            // Act
            Task actual() => handler.HandleAsync(new Register(), new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(actual);
        }

        [Fact]
        public async Task Login_ReturnsUserId()
        {
            // Arrange
            var request = new Login()
            {
                Email = "email",
                Password = "password"
            };

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "email"
            };

            var userRepo = new Mock<IUserWriteRepository>();

            userRepo.Setup(x => x.GetByEmailAsync(request.Email)).ReturnsAsync(user);

            _context.Setup(x => x.Users).Returns(userRepo.Object);

            _hasher.Setup(x => x.Verify(user.PasswordHash, request.Password)).Returns(true);

            var logger = new Mock<ILogger<Login>>();

            _loggerFactory.Setup(x => x.CreateLogger<Login>()).Returns(logger.Object);

            var handler = new AccountCommandHandler(_context.Object, _hasher.Object, _loggerFactory.Object);

            // Act
            Guid userId = await handler.HandleAsync(request, new CancellationToken());

            // Assert
            userId.ShouldBe(user.Id);
        }

        [Fact]
        public async Task Login_WhenUserDoesntExist_Throws()
        {
            // Arrange
            var userRepo = new Mock<IUserWriteRepository>();

            userRepo.Setup(x => x.GetByEmailAsync(It.IsAny<string>()));

            _context.Setup(x => x.Users).Returns(userRepo.Object);

            var handler = new AccountCommandHandler(_context.Object, _hasher.Object, _loggerFactory.Object);

            // Act
            Task actual() => handler.HandleAsync(new Login(), new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ValidationException>(actual);
        }

        [Fact]
        public async Task Login_WhenInvalidPassword_Throws()
        {
            // Arrange
            var userRepo = new Mock<IUserWriteRepository>();

            userRepo.Setup(x => x.GetByEmailAsync(It.IsAny<string>()));

            _context.Setup(x => x.Users).Returns(userRepo.Object);

            _hasher.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var handler = new AccountCommandHandler(_context.Object, _hasher.Object, _loggerFactory.Object);

            // Act
            Task actual() => handler.HandleAsync(new Login(), new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ValidationException>(actual);
        }
    }
}