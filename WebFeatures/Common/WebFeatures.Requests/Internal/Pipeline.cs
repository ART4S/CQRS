using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Requests.Services;

namespace WebFeatures.Requests.Internal
{
    internal abstract class Pipeline<TResponse>
    {
        public abstract Task<TResponse> HandleAsync(IRequest<TResponse> request, IServiceFactory services, CancellationToken cancellationToken);
    }

    internal class PipelineImpl<TRequest, TResponse> : Pipeline<TResponse>
        where TRequest : IRequest<TResponse>
    {
        public override Task<TResponse> HandleAsync(IRequest<TResponse> request, IServiceFactory services, CancellationToken cancellationToken)
        {
            var handler = services.GetService<IRequestHandler<TRequest, TResponse>>();

            RequestDelegate<Task<TResponse>> pipeline =
                () => handler.HandleAsync((TRequest)request, cancellationToken);

            var middlewares = services.GetServices<IRequestMiddleware<TRequest, TResponse>>().Reverse();

            foreach (var middleware in middlewares)
            {
                RequestDelegate<Task<TResponse>> next = pipeline; // for closure
                pipeline = () => middleware.HandleAsync((TRequest)request, next, cancellationToken);
            }

            return pipeline();
        }
    }
}