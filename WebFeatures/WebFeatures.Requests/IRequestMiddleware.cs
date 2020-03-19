using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Requests
{
    public interface IRequestMiddleware<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, Func<TRequest, Task<TResponse>> next, CancellationToken cancellationToken);
    }
}
