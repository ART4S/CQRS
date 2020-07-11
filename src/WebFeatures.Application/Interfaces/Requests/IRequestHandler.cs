using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Application.Interfaces.Requests
{
	public interface IRequestHandler<in TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
	}
}
