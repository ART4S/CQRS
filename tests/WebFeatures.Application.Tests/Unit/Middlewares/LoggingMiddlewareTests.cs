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
        public async Task ShouldReturnNextDelegateResult()
        {
            // Arrange
            var logger = Mock.Of<ILogger<CustomRequest>>();

            var sut = new LoggingMiddleware<CustomRequest, CustomResponse>(logger);

            var expected = new CustomResponse();

            // Act
            CustomResponse actual = await sut.HandleAsync(new CustomRequest(), async () => expected, new CancellationToken());

            // Assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task ShouldCallLoggerBeforeAndAfterNextDelegate()
        {
            // Arrange
            var messages = new List<string>();

            var logger = new Mock<ILogger<CustomRequest>>();

            logger.Setup(x => x.LogInformation(
                    It.IsAny<string>(),
                    It.IsAny<object[]>()))
                .Callback(() => messages.Add("logger"));

            RequestDelegate<Task<CustomResponse>> next = async () =>
            {
                messages.Add("next");

                return new CustomResponse();
            };

            var sut = new LoggingMiddleware<CustomRequest, CustomResponse>(logger.Object);

            // Act
            await sut.HandleAsync(new CustomRequest(), next, new CancellationToken());

            // Assert
            messages.Should().Equal(new[] { "logger", "next", "logger" });
        }
    }
}