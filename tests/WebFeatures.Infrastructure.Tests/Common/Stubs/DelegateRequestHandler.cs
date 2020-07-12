using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Infrastructure.Tests.Common.Stubs
{
    public class DelegateRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly HandleDelegate _handler;

        public DelegateRequestHandler(HandleDelegate handler)
        {
            _handler = handler;
        }

        public Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken)
        {
            return _handler(request, cancellationToken);
        }

        public delegate Task<TResponse> HandleDelegate(TRequest request, CancellationToken cancellationToken);
    }
}
