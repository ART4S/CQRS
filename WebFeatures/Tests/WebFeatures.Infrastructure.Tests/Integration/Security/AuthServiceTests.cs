using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Infrastructure.Security;
using WebFeatures.Infrastructure.Tests.Common.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Security
{
    [Collection("PostgreSqlDatabase")]
    public class AuthServiceTests
    {
        private readonly PostgreSqlDatabaseFixture _db;

        public AuthServiceTests(PostgreSqlDatabaseFixture db)
        {
            _db = db;
        }

        private AuthService CreateDefaultAuthService()
        {
            var stubContext = new Mock<IWriteDbContext>();
            stubContext.Setup(x => x.Connection).Returns(() => _db.Connection);

            return new AuthService(stubContext.Object);
        }

        [Fact]
        public async Task UserHasPermission_ReturnsTrue_WhenUserHasPermission()
        {
            // Arrange
            AuthService authService = CreateDefaultAuthService();

            Guid userId = new Guid("d5d60a37-82a1-4910-af87-16049bd4ff03");
            string permission = "products_create";

            // Act
            bool result = await authService.UserHasPermissionAsync(userId, permission);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task UserHasPermission_ReturnsFalse_WhenUserDoesntHavePermission()
        {
            // Arrange
            AuthService authService = CreateDefaultAuthService();

            Guid userId = new Guid("7f46fdda-8ab7-48eb-b713-2970b5038485");
            string permission = "products_create";

            // Act
            bool result = await authService.UserHasPermissionAsync(userId, permission);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task UserHasPermission_ReturnsFalse_WhenPassedInvalidPermissionName()
        {
            // Arrange
            AuthService authService = CreateDefaultAuthService();

            Guid userId = new Guid("d5d60a37-82a1-4910-af87-16049bd4ff03");
            string permission = "products_creat";

            // Act
            bool result = await authService.UserHasPermissionAsync(userId, permission);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task UserHasPermission_ReturnsFalse_WhenPassedNonexistentUserId()
        {
            // Arrange
            AuthService authService = CreateDefaultAuthService();

            Guid userId = Guid.NewGuid();
            string permission = "products_create";

            // Act
            bool result = await authService.UserHasPermissionAsync(userId, permission);

            // Assert
            result.ShouldBeFalse();
        }
    }
}