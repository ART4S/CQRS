using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Application.Middlewares;
using WebFeatures.Application.Tests.Common.Stubs.Requests;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Middlewares
{
    public class PerformanceMiddlewareTests
    {
        [Fact]
        public async Task ShouldReturnNextDelegateResult()
        {
            // Arrange
            var logger = Mock.Of<ILogger<CustomRequest>>();

            var sut = new PerformanceMiddleware<CustomRequest, CustomResponse>(logger);

            var expected = new CustomResponse();

            // Act
            CustomResponse actual = await sut.HandleAsync(new CustomRequest(), async () => expected, new CancellationToken());

            // Assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task ShouldNotCallLogger_WhenRequestTimeIsLessThanMaxAcceptableTime()
        {
            // Arrange
            var logger = new Mock<ILogger<CustomRequest>>();

            var sut = new PerformanceMiddleware<CustomRequest, CustomResponse>(logger.Object);

            // Act
            await sut.HandleAsync(new CustomRequest(), async () => new CustomResponse(), new CancellationToken());

            // Assert
            logger.Verify(x => x.LogWarning(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ShouldLogWarning_WhenRequestTimeIsGreatherThanMaxAcceptableTime()
        {
            // Arrange
            var logger = new Mock<ILogger<CustomRequest>>();

            var sut = new PerformanceMiddleware<CustomRequest, CustomResponse>(logger.Object);

            const int maxAcceptableTime = 500;

            RequestDelegate<Task<CustomResponse>> next = async () =>
            {
                await Task.Delay(maxAcceptableTime);

                return new CustomResponse();
            };

            // Act
            await sut.HandleAsync(new CustomRequest(), next, new CancellationToken());

            // Assert
            logger.Verify(x => x.LogWarning(It.IsAny<string>()), Times.Once);
        }
    }
}