using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Requests
{
    public interface IRequestMiddleware<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, Func<TRequest, Task<TResponse>> next, CancellationToken cancellationToken);
    }

    public interface IQueryMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse> { }
    public interface ICommandMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse> { }
}
