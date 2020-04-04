using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Requests.Internal
{
    internal abstract class Pipeline<TResponse>
    {
        public abstract Task<TResponse> HandleAsync(IRequest<TResponse> request, IServiceProvider services, CancellationToken cancellationToken);
    }
}
