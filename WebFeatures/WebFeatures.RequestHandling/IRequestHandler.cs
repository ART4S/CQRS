using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.RequestHandling
{
    public interface IRequestHandler<in TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}
