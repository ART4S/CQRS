using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Infrastructure.Requests;
using WebFeatures.Infrastructure.Tests.Common.Stubs;
using WebFeatures.Infrastructure.Tests.Common.Utils;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit.Requests
{
    public class RequestMediatorTests
    {
        private readonly Mock<IRequestHandler<TestRequest, TestResult>> _handler;
        private readonly Mock<IServiceProvider> _serviceProvider;

        public RequestMediatorTests()
        {
            _handler = new Mock<IRequestHandler<TestRequest, TestResult>>();
            _serviceProvider = new Mock<IServiceProvider>();
        }

        [Fact]
        public async Task SendAsync_WhenOneHandler_CallsHandlerOnce()
        {
            // Arrange
            _serviceProvider.Setup(x => x.GetService(
                    typeof(IRequestHandler<TestRequest, TestResult>)))
                .Returns(_handler.Object);

            _serviceProvider.Setup(x => x.GetService(
                    typeof(IEnumerable<IRequestMiddleware<TestRequest, TestResult>>)))
                .Returns(new IRequestMiddleware<TestRequest, TestResult>[0]);

            var mediator = new RequestMediator(_serviceProvider.Object);

            var request = new TestRequest();

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
                    typeof(IEnumerable<IRequestMiddleware<TestRequest, TestResult>>)))
                .Returns(new IRequestMiddleware<TestRequest, TestResult>[0]);

            var mediator = new RequestMediator(_serviceProvider.Object);

            var request = new TestRequest();

            // Act
            Task actual() => mediator.SendAsync(request);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(actual);
        }

        [Fact]
        public async Task SendAsync_CallsMiddlewaresAccordingRegistrationOrder()
        {
            // Arrange
            var callChecker = new CallChecker();

            _serviceProvider.Setup(x => x.GetService(
                    typeof(IRequestHandler<TestRequest, TestResult>)))
                .Returns(new TestRequestHandler(callChecker));

            _serviceProvider.Setup(x => x.GetService(
                    typeof(IEnumerable<IRequestMiddleware<TestRequest, TestResult>>)))
                .Returns(new IRequestMiddleware<TestRequest, TestResult>[]
                {
                    new OutherTestMiddleware(callChecker),
                    new InnerTestMiddleware(callChecker)
                });

            var mediator = new RequestMediator(_serviceProvider.Object);

            // Act
            await mediator.SendAsync(new TestRequest());

            // Assert
            callChecker.Messages.ShouldBe(new[]
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