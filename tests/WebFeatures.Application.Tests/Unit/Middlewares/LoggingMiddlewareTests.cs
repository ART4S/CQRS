using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Application.Middlewares;
using WebFeatures.Application.Tests.Common.Stubs;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Middlewares
{
    public class LoggingMiddlewareTests
    {
        private readonly Mock<ILogger<BoolRequest>> _logger;

        public LoggingMiddlewareTests()
        {
            _logger = new Mock<ILogger<BoolRequest>>();
        }

        [Fact]
        public async Task HandleAsync_ShouldCallLoggerBeforeAndAfterNextDelegate()
        {
            // Arrange
            var messages = new List<string>();

            var logger = new Mock<ILogger<BoolRequest>>();

            _logger.Setup(x => x.LogInformation(
                    It.IsAny<string>(),
                    It.IsAny<object[]>()))
                .Callback(() => messages.Add("logger"));

            RequestDelegate<Task<bool>> next = async () =>
            {
                messages.Add("next");

                return true;
            };

            var middleware = new LoggingMiddleware<BoolRequest, bool>(_logger.Object);

            // Act
            await middleware.HandleAsync(new BoolRequest(), next, new CancellationToken());

            // Assert
            messages.Should().Equal(new[] { "logger", "next", "logger" });
        }
    }
}