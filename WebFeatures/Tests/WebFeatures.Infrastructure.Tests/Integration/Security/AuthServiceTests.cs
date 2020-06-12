using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Infrastructure.Security;
using WebFeatures.Infrastructure.Tests.Helpers.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Security
{
    [Collection("PostgreSqlDatabase")]
    public class AuthServiceTests
    {
        private readonly AuthService _authService;

        public AuthServiceTests(PostgreSqlDatabaseFixture db)
        {
            var contextMock = new Mock<IWriteDbContext>();
            contextMock.Setup(x => x.Connection).Returns(() => db.Connection);

            _authService = new AuthService(contextMock.Object);
        }

        [Fact]
        public async Task UserHasPermission_ReturnsTrue_WhenUserHasPermission()
        {
            // Arrange
            Guid userId = new Guid("d5d60a37-82a1-4910-af87-16049bd4ff03");
            string permission = "products_create";

            // Act
            bool result = await _authService.UserHasPermissionAsync(userId, permission);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task UserHasPermission_ReturnsFalse_WhenUserDoesntHavePermission()
        {
            // Arrange
            Guid userId = new Guid("7f46fdda-8ab7-48eb-b713-2970b5038485");
            string permission = "products_create";

            // Act
            bool result = await _authService.UserHasPermissionAsync(userId, permission);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task UserHasPermission_ReturnsFalse_WhenPassedInvalidPermissionName()
        {
            // Arrange
            Guid userId = new Guid("d5d60a37-82a1-4910-af87-16049bd4ff03");
            string permission = "products_creat";

            // Act
            bool result = await _authService.UserHasPermissionAsync(userId, permission);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task UserHasPermission_ReturnsFalse_WhenPassedNonexistentUserId()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string permission = "products_create";

            // Act
            bool result = await _authService.UserHasPermissionAsync(userId, permission);

            // Assert
            result.ShouldBeFalse();
        }
    }
}