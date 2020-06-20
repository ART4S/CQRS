using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Application.Interfaces.Requests
{
    public interface IRequestMiddleware<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, RequestDelegate<Task<TResponse>> next, CancellationToken cancellationToken);
    }

    public delegate TResponse RequestDelegate<TResponse>();
}