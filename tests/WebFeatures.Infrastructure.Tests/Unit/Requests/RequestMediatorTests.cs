using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Infrastructure.Requests;
using WebFeatures.Infrastructure.Tests.Common.TestObjects;
using WebFeatures.Infrastructure.Tests.Common.Utils;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit.Requests
{
    public class RequestMediatorTests
    {
        [Fact]
        public async Task SendAsync_WhenOneHandler_PassesSameRequest()
        {
            // Arrange
            var request = new TestRequest();

            var handler = new Mock<IRequestHandler<TestRequest, TestResult>>();

            handler.Setup(x => x.HandleAsync(request, It.IsAny<CancellationToken>()));

            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider.Setup(x => x.GetService(
                    typeof(IRequestHandler<TestRequest, TestResult>)))
                .Returns(handler.Object);

            serviceProvider.Setup(x => x.GetService(
                    typeof(IEnumerable<IRequestMiddleware<TestRequest, TestResult>>)))
                .Returns(new IRequestMiddleware<TestRequest, TestResult>[0]);

            var mediator = new RequestMediator(serviceProvider.Object);

            // Act
            await mediator.SendAsync(request);

            // Assert
            handler.Verify();
        }

        [Fact]
        public async Task SendAsync_WhenHandlerHasNotBeenRegistered_Throws()
        {
            // Arrange
            var request = new TestRequest();

            var handler = new Mock<IRequestHandler<TestRequest, TestResult>>();

            handler.Setup(x => x.HandleAsync(request, It.IsAny<CancellationToken>()));

            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider.Setup(x => x.GetService(
                    typeof(IEnumerable<IRequestMiddleware<TestRequest, TestResult>>)))
                .Returns(new IRequestMiddleware<TestRequest, TestResult>[0]);

            var mediator = new RequestMediator(serviceProvider.Object);

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

            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider.Setup(x => x.GetService(
                    typeof(IRequestHandler<TestRequest, TestResult>)))
                .Returns(new TestRequestHandler(callChecker));

            serviceProvider.Setup(x => x.GetService(
                    typeof(IEnumerable<IRequestMiddleware<TestRequest, TestResult>>)))
                .Returns(new IRequestMiddleware<TestRequest, TestResult>[]
                {
                    new OutherTestMiddleware(callChecker),
                    new InnerTestMiddleware(callChecker)
                });

            var mediator = new RequestMediator(serviceProvider.Object);

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