using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Events;
using WebFeatures.Infrastructure.Events;
using WebFeatures.Infrastructure.Tests.Common.Stubs;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit.Events
{
    public class EventMediatorTests
    {
        [Fact]
        public async Task PublishAsync_PassesSameEventToEventHandler()
        {
            // Arrange
            var serviceProvider = new Mock<IServiceProvider>();

            var handler = new Mock<IEventHandler<CustomEvent>>();

            serviceProvider.Setup(x => x.GetService(
                    typeof(IEnumerable<IEventHandler<CustomEvent>>)))
                .Returns(new[] { handler.Object });

            var mediator = new EventMediator(serviceProvider.Object);

            var eve = new CustomEvent();

            // Act
            await mediator.PublishAsync(eve);

            // Assert
            handler.Verify(x => x.HandleAsync(eve, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}