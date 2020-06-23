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
            var logger = Mock.Of<ILogger<TestRequest>>();

            var middleware = new LoggingMiddleware<TestRequest, TestResult>(logger);

            var expected = new TestResult();

            // Act
            TestResult actual = await middleware.HandleAsync(new TestRequest(), async () => expected, new CancellationToken());

            // Assert
            actual.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task HandleAsync_ShouldCallLoggerBeforeAndAfterNextDelegate()
        {
            // Arrange
            var messages = new List<string>();

            var logger = new Mock<ILogger<TestRequest>>();

            logger.Setup(x => x.LogInformation(
                    It.IsAny<string>(),
                    It.IsAny<object[]>()))
                .Callback(() => messages.Add("logger"));

            RequestDelegate<Task<TestResult>> next = async () =>
            {
                messages.Add("next");

                return new TestResult();
            };

            var middleware = new LoggingMiddleware<TestRequest, TestResult>(logger.Object);

            // Act
            await middleware.HandleAsync(new TestRequest(), next, new CancellationToken());

            // Assert
            messages.Should().Equal(new[] { "logger", "next", "logger" });
        }
    }
}