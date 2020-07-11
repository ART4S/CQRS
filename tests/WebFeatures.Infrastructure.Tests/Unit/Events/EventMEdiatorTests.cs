using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using WebFeatures.Application.Interfaces.Events;
using WebFeatures.Infrastructure.Events;
using WebFeatures.Infrastructure.Tests.Common.Stubs;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit.Events
{
	public class EventMediatorTests
	{
		[Fact]
		public async Task ShouldPassSameEventToEventHandler()
		{
			// Arrange
			var serviceProvider = new Mock<IServiceProvider>();

			var handler = new Mock<IEventHandler<CustomEvent>>();

			serviceProvider.Setup(x => x.GetService(
					typeof(IEnumerable<IEventHandler<CustomEvent>>)))
			   .Returns(new[] {handler.Object});

			EventMediator sut = new EventMediator(serviceProvider.Object);

			CustomEvent eve = new CustomEvent();

			// Act
			await sut.PublishAsync(eve);

			// Assert
			handler.Verify(x => x.HandleAsync(eve, It.IsAny<CancellationToken>()), Times.Once);
		}
	}
}
