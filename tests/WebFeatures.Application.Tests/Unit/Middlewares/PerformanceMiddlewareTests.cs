using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Application.Middlewares;
using WebFeatures.Application.Tests.Common.Stubs.Requests;
using Xunit;
using CustomResult = WebFeatures.Application.Tests.Common.Stubs.Requests.CustomResult;

namespace WebFeatures.Application.Tests.Unit.Middlewares
{
    public class PerformanceMiddlewareTests
    {
        [Fact]
        public async Task HandleAsync_ShouldReturnNextDelegateResult()
        {
            // Arrange
            var logger = Mock.Of<ILogger<CustomRequest>>();

            var middleware = new PerformanceMiddleware<CustomRequest, CustomResult>(logger);

            var expected = new CustomResult();

            // Act
            CustomResult actual = await middleware.HandleAsync(new CustomRequest(), async () => expected, new CancellationToken());

            // Assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task HandleAsync_WhenRequestTimeIsLessThanMaxAcceptableTime_ShouldNotCallLogger()
        {
            // Arrange
            var logger = new Mock<ILogger<CustomRequest>>();

            var middleware = new PerformanceMiddleware<CustomRequest, CustomResult>(logger.Object);

            // Act
            await middleware.HandleAsync(new CustomRequest(), async () => new CustomResult(), new CancellationToken());

            // Assert
            logger.Verify(x => x.LogWarning(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task HandleAsync_WhenRequestTimeIsGreatherThanMaxAcceptableTime_ShouldLogWarning()
        {
            // Arrange
            var logger = new Mock<ILogger<CustomRequest>>();

            var middleware = new PerformanceMiddleware<CustomRequest, CustomResult>(logger.Object);

            const int maxAcceptableTime = 500;

            RequestDelegate<Task<CustomResult>> next = async () =>
            {
                await Task.Delay(maxAcceptableTime);

                return new CustomResult();
            };

            // Act
            await middleware.HandleAsync(new CustomRequest(), next, new CancellationToken());

            // Assert
            logger.Verify(x => x.LogWarning(It.IsAny<string>()), Times.Once);
        }
    }
}