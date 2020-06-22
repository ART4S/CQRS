using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Application.Middlewares;
using WebFeatures.Application.Tests.Common.Stubs;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Middlewares
{
    public class PerformanceMiddlewareTests
    {
        private readonly Mock<ILogger<BoolRequest>> _logger;

        public PerformanceMiddlewareTests()
        {
            _logger = new Mock<ILogger<BoolRequest>>();
        }

        [Fact]
        public async Task HandleAsync_ShouldCallNextDelegate()
        {
            // Arrange
            var middleware = new PerformanceMiddleware<BoolRequest, bool>(_logger.Object);

            // Act
            bool result = await middleware.HandleAsync(new BoolRequest(), async () => true, new CancellationToken());

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task HandleAsync_WhenRequestTimeIsLessThanMaxAcceptableTime_ShouldNotCallLogger()
        {
            // Arrange
            var middleware = new PerformanceMiddleware<BoolRequest, bool>(_logger.Object);

            // Act
            await middleware.HandleAsync(new BoolRequest(), async () => true, new CancellationToken());

            // Assert
            _logger.Verify(x => x.LogWarning(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task HandleAsync_WhenRequestTimeIsGreatherThanMaxAcceptableTime_ShouldCallLogger()
        {
            // Arrange
            var middleware = new PerformanceMiddleware<BoolRequest, bool>(_logger.Object);

            const int maxAcceptableTime = 500;

            RequestDelegate<Task<bool>> next = async () =>
            {
                await Task.Delay(maxAcceptableTime);

                return true;
            };

            // Act
            await middleware.HandleAsync(new BoolRequest(), next, new CancellationToken());

            // Assert
            _logger.Verify(x => x.LogWarning(It.IsAny<string>()), Times.Once);
        }
    }
}