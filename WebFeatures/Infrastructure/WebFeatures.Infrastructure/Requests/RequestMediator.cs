using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Infrastructure.Requests
{
    internal class RequestMediator : IRequestMediator
    {
        private static readonly ConcurrentDictionary<Type, object> PipelinesCashe
            = new ConcurrentDictionary<Type, object>();

        private readonly IServiceProvider _services;

        public RequestMediator(IServiceProvider services)
        {
            _services = services;
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var pipeline = (Pipeline<TResponse>)PipelinesCashe.GetOrAdd(
                request.GetType(),
                _ =>
                {
                    Type pipelineType = typeof(Pipeline<,>).MakeGenericType(
                        request.GetType(),
                        typeof(TResponse));

                    return Activator.CreateInstance(pipelineType);
                });

            return pipeline.HandleAsync(request, _services, cancellationToken);
        }
    }
}