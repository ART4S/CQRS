using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Infrastructure.Requests;
using WebFeatures.Infrastructure.Tests.Common.Stubs;
using WebFeatures.Infrastructure.Tests.Common.Utils;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit.Requests
{
    public class RequestMediatorTests
    {
        private readonly Mock<IRequestHandler<CustomRequest, CustomResult>> _handler;
        private readonly Mock<IServiceProvider> _serviceProvider;

        public RequestMediatorTests()
        {
            _handler = new Mock<IRequestHandler<CustomRequest, CustomResult>>();
            _serviceProvider = new Mock<IServiceProvider>();
        }

        [Fact]
        public async Task SendAsync_WhenOneHandler_CallsHandlerOnce()
        {
            // Arrange
            _serviceProvider.Setup(x => x.GetService(
                    typeof(IRequestHandler<CustomRequest, CustomResult>)))
                .Returns(_handler.Object);

            _serviceProvider.Setup(x => x.GetService(
                    typeof(IEnumerable<IRequestMiddleware<CustomRequest, CustomResult>>)))
                .Returns(new IRequestMiddleware<CustomRequest, CustomResult>[0]);

            var mediator = new RequestMediator(_serviceProvider.Object);

            var request = new CustomRequest();

            // Act
            await mediator.SendAsync(request);

            // Assert
            _handler.Verify(x => x.HandleAsync(request, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SendAsync_WhenHandlerHasNotBeenRegistered_Throws()
        {
            // Arrange
            _serviceProvider.Setup(x => x.GetService(
                    typeof(IEnumerable<IRequestMiddleware<CustomRequest, CustomResult>>)))
                .Returns(new IRequestMiddleware<CustomRequest, CustomResult>[0]);

            var mediator = new RequestMediator(_serviceProvider.Object);

            var request = new CustomRequest();

            // Act
            Func<Task> actual = () => mediator.SendAsync(request);

            // Assert
            await actual.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task SendAsync_CallsMiddlewaresAccordingRegistrationOrder()
        {
            // Arrange
            var callChecker = new CallChecker();

            _serviceProvider.Setup(x => x.GetService(
                    typeof(IRequestHandler<CustomRequest, CustomResult>)))
                .Returns(new CustomRequestHandler(callChecker));

            _serviceProvider.Setup(x => x.GetService(
                    typeof(IEnumerable<IRequestMiddleware<CustomRequest, CustomResult>>)))
                .Returns(new IRequestMiddleware<CustomRequest, CustomResult>[]
                {
                    new OutherMiddleware(callChecker),
                    new InnerMiddleware(callChecker)
                });

            var mediator = new RequestMediator(_serviceProvider.Object);

            // Act
            await mediator.SendAsync(new CustomRequest());

            // Assert
            callChecker.Messages.Should().Equal(new[]
            {
                "Outher started",
                "Inner started",
                "Handler finished",
                "Inner finished",
                "Outher finished"
            });
        }
    }
}