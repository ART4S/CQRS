using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Features.Permissions.Requests.Queries;
using WebFeatures.Application.Tests.Common.Base;
using Xunit;

namespace WebFeatures.Application.Tests.Integration.Features.Permissions
{
    public class PermissionQueryHandlerTests : IntegrationTestBase
    {
        [Theory]
        [MemberData(nameof(AdminPermissions))]
        [MemberData(nameof(UserPermissions))]
        public async Task UserHasPermission_ReturnsTrue((string Email, string Password) user, string permission)
        {
            // Arrange
            var request = new UserHasPermission() { Permission = permission };

            // Act
            await AuthenticateAsync(user.Email, user.Password);

            bool result = await Mediator.SendAsync(request);

            // Assert
            result.Should().BeTrue();
        }

        public static IEnumerable<object[]> AdminPermissions
        {
            get
            {
                var admin = (Email: "admin@mail.com", Password: "12345");

                // Products
                yield return new object[] { admin, PermissionConstants.Products.Create };
                yield return new object[] { admin, PermissionConstants.Products.Update };
                yield return new object[] { admin, PermissionConstants.Products.Delete };

                // ProductRviews
                yield return new object[] { admin, PermissionConstants.ProductReviews.Create };

                // ProductComments
                yield return new object[] { admin, PermissionConstants.ProductComments.Create };
            }
        }

        public static IEnumerable<object[]> UserPermissions
        {
            get
            {
                var user = (Email: "user@mail.com", Password: "12345");

                // ProductComments
                yield return new object[] { user, PermissionConstants.ProductComments.Create };
            }
        }

        [Fact]
        public async Task UserHasPermission_WhenUserIsNotAuthenticated_ReturnsFalse()
        {
            // Arrange
            var request = new UserHasPermission() { Permission = PermissionConstants.Products.Create };

            // Act
            bool result = await Mediator.SendAsync(request);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UserHasPermission_WhenUserDoesntHavePermission_ReturnsFalse()
        {
            // Arrange
            var request = new UserHasPermission() { Permission = PermissionConstants.Products.Create };

            // Act
            await AuthenticateAsync("user@mail.com", "12345");

            bool result = await Mediator.SendAsync(request);

            // Assert
            result.Should().BeFalse();
        }
    }
}