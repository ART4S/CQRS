using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Application.Middlewares;
using WebFeatures.Application.Tests.Common.Stubs.Requests;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Middlewares
{
    public class LoggingMiddlewareTests
    {
        [Fact]
        public async Task HandleAsync_ShouldReturnNextDelegateResult()
        {
            // Arrange
            var logger = Mock.Of<ILogger<CustomRequest>>();

            var sut = new LoggingMiddleware<CustomRequest, CustomResult>(logger);

            var expected = new CustomResult();

            // Act
            CustomResult actual = await sut.HandleAsync(new CustomRequest(), async () => expected, new CancellationToken());

            // Assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task HandleAsync_ShouldCallLoggerBeforeAndAfterNextDelegate()
        {
            // Arrange
            var messages = new List<string>();

            var logger = new Mock<ILogger<CustomRequest>>();

            logger.Setup(x => x.LogInformation(
                    It.IsAny<string>(),
                    It.IsAny<object[]>()))
                .Callback(() => messages.Add("logger"));

            RequestDelegate<Task<CustomResult>> next = async () =>
            {
                messages.Add("next");

                return new CustomResult();
            };

            var sut = new LoggingMiddleware<CustomRequest, CustomResult>(logger.Object);

            // Act
            await sut.HandleAsync(new CustomRequest(), next, new CancellationToken());

            // Assert
            messages.Should().Equal(new[] { "logger", "next", "logger" });
        }
    }
}