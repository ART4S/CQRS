using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Infrastructure.Tests.Common.Stubs
{
    public class DelegateMiddleware<TRequest, TResult> : IRequestMiddleware<TRequest, TResult>
    {
        private readonly HandleDelegate _handle;

        public DelegateMiddleware(HandleDelegate handle)
        {
            _handle = handle;
        }

        public Task<TResult> HandleAsync(TRequest request, RequestDelegate<Task<TResult>> next, CancellationToken cancellationToken)
        {
            return _handle(request, next, cancellationToken);
        }

        public delegate Task<TResult> HandleDelegate(TRequest request, RequestDelegate<Task<TResult>> next, CancellationToken cancellationToken);
    }
}
