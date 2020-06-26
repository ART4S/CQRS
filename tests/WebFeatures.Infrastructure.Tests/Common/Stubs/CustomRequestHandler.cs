using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Infrastructure.Tests.Common.Utils;

namespace WebFeatures.Infrastructure.Tests.Common.Stubs
{
    internal class CustomRequestHandler : IRequestHandler<CustomRequest, CustomResult>
    {
        private readonly CallChecker _checker;

        public CustomRequestHandler(CallChecker checker)
        {
            _checker = checker;
        }

        public Task<CustomResult> HandleAsync(CustomRequest request, CancellationToken cancellationToken)
        {
            _checker.Messages.Add("Handler finished");

            return Task.FromResult(new CustomResult());
        }
    }
}
