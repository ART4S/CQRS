using Bogus;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Authorization.UserHasPermission;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Application.Interfaces.Services;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Features.Authorization.UserHasPermission
{
    public class UserHasPermissionQueryTests
    {
        private readonly Mock<IAuthService> _authService;
        private readonly Mock<ICurrentUserService> _currentUser;

        public UserHasPermissionQueryTests()
        {
            _authService = new Mock<IAuthService>();
            _currentUser = new Mock<ICurrentUserService>();
        }

        [Fact]
        public async Task HandleAsync_WhenUserIsNotAuthenticated_ReturnsFalse()
        {
            // Arrange
            _currentUser.Setup(x => x.IsAuthenticated).Returns(false);

            var sut = new UserHasPermissionQueryHandler(_authService.Object, _currentUser.Object);

            // Act
            bool isAuthorized = await sut.HandleAsync(new UserHasPermissionQuery(), new CancellationToken());

            // Assert
            isAuthorized.Should().BeFalse();
        }

        [Fact]
        public async Task HandleAsync_WhenUserIsAuthenticated_ReturnsTrue()
        {
            // Arrange
            _currentUser.Setup(x => x.IsAuthenticated).Returns(true);

            Guid userId = new Randomizer().Guid();

            _currentUser.Setup(x => x.UserId).Returns(userId);

            var request = new UserHasPermissionQuery() { Permission = new Randomizer().Utf16String() };

            _authService.Setup(x => x.UserHasPermissionAsync(userId, request.Permission)).ReturnsAsync(true);

            var sut = new UserHasPermissionQueryHandler(_authService.Object, _currentUser.Object);

            // Act
            bool isAuthorized = await sut.HandleAsync(request, new CancellationToken());

            // Assert
            isAuthorized.Should().BeTrue();
        }
    }
}