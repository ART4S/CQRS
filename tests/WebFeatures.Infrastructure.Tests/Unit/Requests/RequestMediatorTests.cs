using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Infrastructure.Requests;
using WebFeatures.Infrastructure.Tests.Common.Stubs;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit.Requests
{
    public class RequestMediatorTests
    {
        private readonly Mock<IRequestHandler<CustomRequest, CustomResponse>> _handler;
        private readonly Mock<IServiceProvider> _serviceProvider;

        public RequestMediatorTests()
        {
            _handler = new Mock<IRequestHandler<CustomRequest, CustomResponse>>();
            _serviceProvider = new Mock<IServiceProvider>();
        }

        [Fact]
        public async Task ShouldCallHandler()
        {
            // Arrange
            _serviceProvider.Setup(x => x.GetService(
                    typeof(IRequestHandler<CustomRequest, CustomResponse>)))
                .Returns(_handler.Object);

            _serviceProvider.Setup(x => x.GetService(
                    typeof(IEnumerable<IRequestMiddleware<CustomRequest, CustomResponse>>)))
                .Returns(new IRequestMiddleware<CustomRequest, CustomResponse>[0]);

            var sut = new RequestMediator(_serviceProvider.Object);

            var request = new CustomRequest();

            var token = new CancellationToken();

            // Act
            await sut.SendAsync(request, token);

            // Assert
            _handler.Verify(x => x.HandleAsync(request, token), Times.Once);
        }

        [Fact]
        public void ShouldThrow_WhenHandlerIsMissing()
        {
            // Arrange
            _serviceProvider.Setup(x => x.GetService(
                    typeof(IEnumerable<IRequestMiddleware<CustomRequest, CustomResponse>>)))
                .Returns(new IRequestMiddleware<CustomRequest, CustomResponse>[0]);

            var sut = new RequestMediator(_serviceProvider.Object);

            // Act
            Func<Task> act = () => sut.SendAsync(new CustomRequest());

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public async Task ShouldCallMiddlewaresAccordingRegistrationOrder()
        {
            // Arrange
            var messages = new List<string>();

            var handler = new DelegateRequestHandler<CustomRequest, CustomResponse>(async (request, token) =>
            {
                messages.Add("handler");

                return new CustomResponse();
            });

            _serviceProvider.Setup(x => x.GetService(
                    typeof(IRequestHandler<CustomRequest, CustomResponse>)))
                .Returns(handler);

            var outher = new DelegateMiddleware<CustomRequest, CustomResponse>(async (request, next, token) =>
            {
                messages.Add("outher started");

                CustomResponse response = await next();

                messages.Add("outher finished");

                return response;
            });

            var inner = new DelegateMiddleware<CustomRequest, CustomResponse>(async (request, next, token) =>
            {
                messages.Add("inner started");

                CustomResponse response = await next();

                messages.Add("inner finished");

                return response;
            });

            _serviceProvider.Setup(x => x.GetService(
                    typeof(IEnumerable<IRequestMiddleware<CustomRequest, CustomResponse>>)))
                .Returns(new IRequestMiddleware<CustomRequest, CustomResponse>[]
                {
                    outher,
                    inner,
                });

            var sut = new RequestMediator(_serviceProvider.Object);

            // Act
            await sut.SendAsync(new CustomRequest());

            // Assert
            messages.Should().Equal(new[]
            {
                "outher started",
                "inner started",
                "handler",
                "inner finished",
                "outher finished"
            });
        }
    }
}