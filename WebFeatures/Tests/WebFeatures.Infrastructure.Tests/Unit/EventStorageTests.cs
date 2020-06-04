using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Infrastructure.Events;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit
{
    public class EventStorageTests
    {
        [Fact]
        public void PublishAllAsync_ShouldCallHandlers()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var storage = new EventStorage(serviceProviderMock.Object);
            var eventMock = new Mock<IEvent>();

            // Act
            storage.Add(eventMock.Object);
        }

        [Fact]
        public void PublishAllAsync_ShouldPassSameEvent()
        {
            //// Arrange
            //var serviceProviderMock = new Mock<IServiceProvider>();
            //serviceProviderMock.Setup(x => x.GetService(It.Is<Type>(y => y == typeof(IEn))))
            //var storage = new EventStorage(serviceProviderMock.Object);
            //var eventMock = new Mock<IEvent>();

            //// Act
            //storage.Add(eventMock.Object);
        }
    }

    internal class TestEvent : IEvent
    {

    }

    internal class TestEventHandler : IEventHandler<TestEvent>
    {

        private readonly CallCounter _counter;

        public TestEventHandler(CallCounter counter)
        {
            _counter = counter;
        }

        public Task HandleAsync(TestEvent eve, CancellationToken cancellationToken)
        {
            _counter.Increase();
            return Task.CompletedTask;
        }
    }

    internal class CallCounter
    {
        public int CallCount { get; private set; }

        public void Increase()
        {
            CallCount++;
        }
    }
}