using Moq;
using Shouldly;
using System;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Infrastructure.Security;
using WebFeatures.Infrastructure.Tests.Common;
using WebFeatures.Infrastructure.Tests.Common.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Security
{
    public class AuthServiceTests : IntegrationTestBase
    {
        private readonly IDbConnection _connection;

        public AuthServiceTests(DatabaseFixture db) : base(db)
        {
            _connection = db.Connection;
        }

        private AuthService CreateDefaultAuthService()
        {
            var stubContext = new Mock<IWriteDbContext>();
            stubContext.Setup(x => x.Connection).Returns(() => _connection);

            return new AuthService(stubContext.Object);
        }

        [Fact]
        public async Task UserHasPermission_ReturnsTrue_WhenUserHasPermission()
        {
            // Arrange
            AuthService authService = CreateDefaultAuthService();

            Guid userId = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09");
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

            Guid userId = new Guid("5687c80f-d495-460a-aae5-94ea8054ee2c");
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

            Guid userId = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09");
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

            Guid userId = new Guid("3cdd1fc5-4c54-4484-a2e5-95920b79734e");
            string permission = "products_create";

            // Act
            bool result = await authService.UserHasPermissionAsync(userId, permission);

            // Assert
            result.ShouldBeFalse();
        }
    }
}