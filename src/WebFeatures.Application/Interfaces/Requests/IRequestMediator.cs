using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Application.Interfaces.Requests
{
    public interface IRequestMediator
    {
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    }
}