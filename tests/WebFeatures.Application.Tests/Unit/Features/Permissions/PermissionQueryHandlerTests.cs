using Bogus;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Permissions.Handlers;
using WebFeatures.Application.Features.Permissions.Requests.Queries;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Application.Interfaces.Services;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Features.Permissions
{
    public class PermissionQueryHandlerTests
    {
        private readonly Mock<IAuthService> _authService;
        private readonly Mock<ICurrentUserService> _currentUser;

        public PermissionQueryHandlerTests()
        {
            _authService = new Mock<IAuthService>();
            _currentUser = new Mock<ICurrentUserService>();
        }

        [Fact]
        public async Task UserHasPermission_WhenUserIsNotAuthenticated_ReturnsFalse()
        {
            // Arrange
            _currentUser.Setup(x => x.IsAuthenticated).Returns(false);

            PermissionQueryHandler handler = new PermissionQueryHandler(_authService.Object, _currentUser.Object);

            // Act
            bool result = await handler.HandleAsync(new UserHasPermission(), new CancellationToken());

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UserHasPermission_WhenUserIsAuthenticated_ReturnsTrue()
        {
            // Arrange
            _currentUser.Setup(x => x.IsAuthenticated).Returns(true);

            Guid userId = new Faker().Random.Guid();

            _currentUser.Setup(x => x.UserId).Returns(userId);

            var request = new UserHasPermission() { Permission = new Faker().Random.Utf16String() };

            _authService.Setup(x => x.UserHasPermissionAsync(userId, request.Permission)).ReturnsAsync(true);

            PermissionQueryHandler handler = new PermissionQueryHandler(_authService.Object, _currentUser.Object);

            // Act
            bool result = await handler.HandleAsync(request, new CancellationToken());

            // Assert
            result.Should().BeTrue();
        }
    }
}