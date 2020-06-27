using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Infrastructure.DataAccess.QueryExecutors;
using WebFeatures.Infrastructure.Security;
using WebFeatures.Infrastructure.Tests.Common.Base;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Security
{
    public class AuthServiceTests : IntegrationTestBase
    {
        private AuthService CreateDefaultAuthService()
        {
            var context = new Mock<IWriteDbContext>();

            context.Setup(x => x.Connection).Returns(() => Database.Connection);

            return new AuthService(context.Object, new DapperDbExecutor());
        }

        [Fact]
        public async Task UserHasPermission_WhenUserHasPermission_ReturnsTrue()
        {
            // Arrange
            AuthService sut = CreateDefaultAuthService();

            Guid userId = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09");

            string permission = "products_create";

            // Act
            bool result = await sut.UserHasPermissionAsync(userId, permission);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UserHasPermission_WhenUserDoesntHavePermission_ReturnsFalse()
        {
            // Arrange
            AuthService sut = CreateDefaultAuthService();

            Guid userId = new Guid("5687c80f-d495-460a-aae5-94ea8054ee2c");

            string permission = "products_create";

            // Act
            bool result = await sut.UserHasPermissionAsync(userId, permission);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UserHasPermission_WhenInvalidPermissionName_ReturnsFalse()
        {
            // Arrange
            AuthService sut = CreateDefaultAuthService();

            Guid userId = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09");

            string permission = "products_creat";

            // Act
            bool result = await sut.UserHasPermissionAsync(userId, permission);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UserHasPermission_WhenUserDoesntExist_ReturnsFalse()
        {
            // Arrange
            AuthService sut = CreateDefaultAuthService();

            Guid userId = new Guid("3cdd1fc5-4c54-4484-a2e5-95920b79734e");

            string permission = "products_create";

            // Act
            bool result = await sut.UserHasPermissionAsync(userId, permission);

            // Assert
            result.Should().BeFalse();
        }
    }
}