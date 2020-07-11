using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Features.Authorization.UserHasPermission;
using WebFeatures.Application.Tests.Common.Base;
using Xunit;

namespace WebFeatures.Application.Tests.Integration.Features.Authorization.UserHasPermission
{
    public class UserHasPermissionQueryTests : IntegrationTestBase
    {
        [Theory]
        [MemberData(nameof(PermissionTestData.Admin), MemberType = typeof(PermissionTestData))]
        [MemberData(nameof(PermissionTestData.User), MemberType = typeof(PermissionTestData))]
        public async Task ShouldReturnTrue_WhenUserIsAuthenticated((string Email, string Password) user, string permission)
        {
            // Arrange
            var request = new UserHasPermissionQuery() { Permission = permission };

            // Act
            await AuthenticateAsync(user.Email, user.Password);

            bool isAuthorized = await Mediator.SendAsync(request);

            // Assert
            isAuthorized.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnFalse_WhenUserIsNotAuthenticated()
        {
            // Arrange
            var request = new UserHasPermissionQuery() { Permission = PermissionConstants.Products.Create };

            // Act
            bool isAuthorized = await Mediator.SendAsync(request);

            // Assert
            isAuthorized.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldReturnFalse_WhenUserDoesntHavePermission()
        {
            // Arrange
            var request = new UserHasPermissionQuery() { Permission = PermissionConstants.Products.Create };

            // Act
            await AuthenticateAsync("user@mail.com", "12345");

            bool isAuthorized = await Mediator.SendAsync(request);

            // Assert
            isAuthorized.Should().BeFalse();
        }
    }

    internal class PermissionTestData
    {
        public static IEnumerable<object[]> Admin
        {
            get
            {
                var admin = (Email: "admin@mail.com", Password: "12345");

                // Products
                yield return new object[] { admin, PermissionConstants.Products.Create };
                yield return new object[] { admin, PermissionConstants.Products.Update };
                yield return new object[] { admin, PermissionConstants.Products.Delete };

                // ProductReviews
                yield return new object[] { admin, PermissionConstants.ProductReviews.Create };

                // ProductComments
                yield return new object[] { admin, PermissionConstants.ProductComments.Create };
            }
        }

        public static IEnumerable<object[]> User
        {
            get
            {
                var user = (Email: "user@mail.com", Password: "12345");

                // ProductComments
                yield return new object[] { user, PermissionConstants.ProductComments.Create };
            }
        }
    }
}