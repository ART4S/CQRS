using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Requests
{
    internal class RequestMediator : IRequestMediator
    {
        private readonly IServiceProvider _services;
        private static readonly ConcurrentDictionary<Type, object> PipelinesCashe = new ConcurrentDictionary<Type, object>();

        public RequestMediator(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            var pipeline = (Pipeline<TResponse>)PipelinesCashe.GetOrAdd(
                request.GetType(),
                t =>
                {
                    Type pipelineType = typeof(PipelineImpl<,>).MakeGenericType(t, typeof(TResponse));
                    return Activator.CreateInstance(pipelineType);
                });

            return pipeline.HandleAsync(request, _services, cancellationToken);
        }
    }

    internal abstract class Pipeline<TResponse>
    {
        public abstract Task<TResponse> HandleAsync(IRequest<TResponse> request, IServiceProvider services, CancellationToken cancellationToken);
    }

    internal class PipelineImpl<TRequest, TResponse> : Pipeline<TResponse>
        where TRequest : IRequest<TResponse>
    {
        public override Task<TResponse> HandleAsync(IRequest<TResponse> request, IServiceProvider services, CancellationToken cancellationToken)
        {
            var handler = services.GetService<IRequestHandler<TRequest, TResponse>>();
            Func<Task<TResponse>> pipeline = () => handler.HandleAsync((TRequest)request, cancellationToken);

            var middlewares = services.GetServices<IRequestMiddleware<TRequest, TResponse>>().Reverse();

            foreach (var middleware in middlewares)
            {
                Func<Task<TResponse>> next = pipeline; // for closure
                pipeline = () => middleware.HandleAsync((TRequest)request, next, cancellationToken);
            }

            return pipeline();
        }
    }
}
