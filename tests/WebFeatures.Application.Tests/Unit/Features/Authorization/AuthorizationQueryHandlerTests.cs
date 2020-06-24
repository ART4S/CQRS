using Bogus;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Authorization.Handlers;
using WebFeatures.Application.Features.Permissions.Requests.Queries;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Application.Interfaces.Services;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Features.Authorization
{
    public class AuthorizationQueryHandlerTests
    {
        private readonly Mock<IAuthService> _authService;
        private readonly Mock<ICurrentUserService> _currentUser;

        public AuthorizationQueryHandlerTests()
        {
            _authService = new Mock<IAuthService>();
            _currentUser = new Mock<ICurrentUserService>();
        }

        [Fact]
        public async Task UserHasPermission_WhenUserIsNotAuthenticated_ReturnsFalse()
        {
            // Arrange
            _currentUser.Setup(x => x.IsAuthenticated).Returns(false);

            var handler = new AuthorizationQueryHandler(_authService.Object, _currentUser.Object);

            // Act
            bool isAuthorized = await handler.HandleAsync(new UserHasPermission(), new CancellationToken());

            // Assert
            isAuthorized.Should().BeFalse();
        }

        [Fact]
        public async Task UserHasPermission_WhenUserIsAuthenticated_ReturnsTrue()
        {
            // Arrange
            _currentUser.Setup(x => x.IsAuthenticated).Returns(true);

            Guid userId = new Randomizer().Guid();

            _currentUser.Setup(x => x.UserId).Returns(userId);

            var request = new UserHasPermission() { Permission = new Randomizer().Utf16String() };

            _authService.Setup(x => x.UserHasPermissionAsync(userId, request.Permission)).ReturnsAsync(true);

            var handler = new AuthorizationQueryHandler(_authService.Object, _currentUser.Object);

            // Act
            bool isAuthorized = await handler.HandleAsync(request, new CancellationToken());

            // Assert
            isAuthorized.Should().BeTrue();
        }
    }
}