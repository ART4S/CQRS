using Moq;
using Shouldly;
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
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Permissions;
using Xunit;
using ILoggerFactory = WebFeatures.Application.Interfaces.Logging.ILoggerFactory;

namespace WebFeatures.Application.Tests.Unit.Features.Accounts
{
    public class AccountCommandHandlerTests
    {
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

            var hasher = new Mock<IPasswordHasher>();

            hasher.Setup(x => x.ComputeHash(request.Password)).Returns(() => hash);

            var userRepo = new Mock<IUserWriteRepository>();

            userRepo.Setup(x => x.CreateAsync(
                    It.Is<User>(x => x.Email == request.Email && x.Name == request.Name && x.PasswordHash == hash)))
                .Callback<User>(x => x.Id = Guid.NewGuid());

            var roleRepo = new Mock<IRoleWriteRepository>();

            roleRepo.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(() => role);

            var userRoleRepo = new Mock<IWriteRepository<UserRole>>();

            var context = new Mock<IWriteDbContext>();

            context.Setup(x => x.Users).Returns(() => userRepo.Object);
            context.Setup(x => x.Roles).Returns(() => roleRepo.Object);
            context.Setup(x => x.UserRoles).Returns(() => userRoleRepo.Object);

            var logger = new Mock<ILogger<Register>>();

            var loggerFactory = new Mock<ILoggerFactory>();
            loggerFactory.Setup(x => x.CreateLogger<Register>()).Returns(() => logger.Object);

            var handler = new AccountCommandHandler(context.Object, hasher.Object, loggerFactory.Object);

            // Act
            Guid userId = await handler.HandleAsync(request, CancellationToken.None);

            // Assert
            userRepo.Verify();
            userId.ShouldNotBe(default);
        }

        [Fact]
        public async Task Register_WhenRoleForUserDoesntExist_Throws()
        {
            // Arrange
            var hasher = new Mock<IPasswordHasher>();

            var userRepo = new Mock<IUserWriteRepository>();
            var roleRepo = new Mock<IRoleWriteRepository>();

            roleRepo.Setup(x => x.GetByNameAsync(It.IsAny<string>()));

            var context = new Mock<IWriteDbContext>();

            context.Setup(x => x.Users).Returns(() => userRepo.Object);
            context.Setup(x => x.Roles).Returns(() => roleRepo.Object);

            var loggerFactory = new Mock<ILoggerFactory>();

            var handler = new AccountCommandHandler(context.Object, hasher.Object, loggerFactory.Object);

            // Act
            Task actual() => handler.HandleAsync(new Register(), CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(actual);
        }
    }
}