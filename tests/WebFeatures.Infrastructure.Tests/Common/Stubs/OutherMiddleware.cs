using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Infrastructure.Tests.Common.Utils;

namespace WebFeatures.Infrastructure.Tests.Common.Stubs
{
    internal class OutherMiddleware : IRequestMiddleware<CustomRequest, CustomResult>
    {
        private readonly CallChecker _checker;

        public OutherMiddleware(CallChecker checker)
        {
            _checker = checker;
        }

        public async Task<CustomResult> HandleAsync(CustomRequest request, RequestDelegate<Task<CustomResult>> next, CancellationToken cancellationToken)
        {
            _checker.Messages.Add("Outher started");

            CustomResult result = await next();

            _checker.Messages.Add("Outher finished");

            return result;
        }
    }
}
