using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Requests
{
    internal class RequestMediator : IRequestMediator
    {
        private readonly IServiceProvider _services;
        private static readonly ConcurrentDictionary<Type, object> _pipelinesCache = new ConcurrentDictionary<Type, object>();

        public RequestMediator(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            var pipeline = (Pipeline<TResponse>)_pipelinesCache.GetOrAdd(
                request.GetType(),
                t =>
                {
                    Type pipelineType;

                    switch (request)
                    {
                        case IQuery<TResponse> _:
                            pipelineType = typeof(QueryPipeline<,>).MakeGenericType(t, typeof(TResponse));
                            break;
                        case ICommand<TResponse> _:
                            pipelineType = typeof(CommandPipeline<,>).MakeGenericType(t, typeof(TResponse));
                            break;
                        default:
                            pipelineType = typeof(RequestPipeline<,>).MakeGenericType(t, typeof(TResponse));
                            break;
                    }

                    return Activator.CreateInstance(pipelineType);
                });

            return pipeline.HandleAsync(request, _services, cancellationToken);
        }
    }

    internal abstract class Pipeline<TResponse>
    {
        public abstract Task<TResponse> HandleAsync(IRequest<TResponse> request, IServiceProvider services, CancellationToken cancellationToken);
    }

    internal class RequestPipeline<TRequest, TResponse> : Pipeline<TResponse>
    {
        public override Task<TResponse> HandleAsync(IRequest<TResponse> request, IServiceProvider services, CancellationToken cancellationToken)
        {
            var handler = services.GetService<IRequestHandler<TRequest, TResponse>>();
            Func<TRequest, Task<TResponse>> pipeline = req => handler.HandleAsync(req, cancellationToken);

            var middlewares = GetMiddlewares(services).Reverse();

            foreach (var middleware in middlewares)
            {
                var next = pipeline; // for closure
                pipeline = req => middleware.HandleAsync(req, next, cancellationToken);
            }

            return pipeline((TRequest)request);
        }

        protected virtual IEnumerable<IRequestMiddleware<TRequest, TResponse>> GetMiddlewares(IServiceProvider services)
            => services.GetServices<IRequestMiddleware<TRequest, TResponse>>();
    }

    internal class QueryPipeline<TRequest, TResponse> : RequestPipeline<TRequest, TResponse>
    {
        protected override IEnumerable<IRequestMiddleware<TRequest, TResponse>> GetMiddlewares(IServiceProvider services)
            => base.GetMiddlewares(services)
                .Concat(services.GetServices<IQueryMiddleware<TRequest, TResponse>>());
    }

    internal class CommandPipeline<TRequest, TResponse> : RequestPipeline<TRequest, TResponse>
    {
        protected override IEnumerable<IRequestMiddleware<TRequest, TResponse>> GetMiddlewares(IServiceProvider services)
            => base.GetMiddlewares(services)
                .Concat(services.GetServices<ICommandMiddleware<TRequest, TResponse>>());
    }
}
