using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Requests.Internal
{
    internal class RequestMediator : IRequestMediator
    {
        private readonly IServiceProvider _services;
        private static readonly ConcurrentDictionary<Type, object> PipelinesCashe = new ConcurrentDictionary<Type, object>();

        public RequestMediator(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var pipeline = (Pipeline<TResponse>)PipelinesCashe.GetOrAdd(
                request.GetType(),
                _ =>
                {
                    Type pipelineType = typeof(PipelineImpl<,>).MakeGenericType(request.GetType(), typeof(TResponse));
                    return Activator.CreateInstance(pipelineType);
                });

            return pipeline.HandleAsync(request, _services, cancellationToken);
        }
    }
}
