using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebFeatures.Events
{
    internal class EventMediator : IEventMediator
    {
        private readonly IServiceProvider _services;

        public EventMediator(IServiceProvider services)
        {
            _services = services;
        }

        public Task PublishAsync<TEvent>(TEvent eve) where TEvent : IEvent
            => Task.WhenAll(
                _services.GetServices<IEventHandler<TEvent>>()
                    .Select(x => x.HandleAsync(eve)));
    }
}