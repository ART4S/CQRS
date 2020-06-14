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
        public async Task Register_Once()
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

            var mockUserRepo = new Mock<IUserWriteRepository>();
            var mockRoleRepo = new Mock<IRoleWriteRepository>();
            mockRoleRepo.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(() => role);

            var mockUserRoleRepo = new Mock<IWriteRepository<UserRole>>();

            var stubContext = new Mock<IWriteDbContext>();
            stubContext.Setup(x => x.Users).Returns(() => mockUserRepo.Object);
            stubContext.Setup(x => x.Roles).Returns(() => mockRoleRepo.Object);
            stubContext.Setup(x => x.UserRoles).Returns(() => mockUserRoleRepo.Object);

            var mockHasher = new Mock<IPasswordHasher>();

            var mockLogger = new Mock<ILogger<Register>>();

            var mockLoggerFactory = new Mock<ILoggerFactory>();
            mockLoggerFactory.Setup(x => x.CreateLogger<Register>()).Returns(() => mockLogger.Object);

            var handler = new AccountCommandHandler(stubContext.Object, mockHasher.Object, mockLoggerFactory.Object);

            // Act
            Guid result = await handler.HandleAsync(request, CancellationToken.None);

            // Assert
            mockHasher.Verify(x => x.ComputeHash(request.Password), Times.Once);

            mockUserRepo.Verify(x => x.CreateAsync(
                It.Is<User>(u => u.Email == request.Email && u.Name == request.Name)), Times.Once);

            mockRoleRepo.Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once);

            mockUserRoleRepo.Verify(x => x.CreateAsync(It.IsAny<UserRole>()), Times.Once);

            mockLoggerFactory.Verify(x => x.CreateLogger<Register>(), Times.Once);

            mockLogger.Verify(x => x.LogInformation(It.IsAny<string>()), Times.Once);
        }
    }
}