using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.Logging;

namespace WebFeatures.Application.Middlewares
{
    /// <summary>
    /// Publishes events
    /// </summary>
    internal class EventsMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IEventStorage _events;

        public EventsMiddleware(ILogger<TRequest> logger, IEventStorage events)
        {
            _logger = logger;
            _events = events;
        }

        public async Task<TResponse> HandleAsync(TRequest request, RequestDelegate<Task<TResponse>> next, CancellationToken cancellationToken)
        {
            TResponse response = await next();

            try
            {
                await _events.PublishAllAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while publishing events", ex);
                throw;
            }

            return response;
        }
    }
}