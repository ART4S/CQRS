using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Infrastructure.Tests.Common.Utils;

namespace WebFeatures.Infrastructure.Tests.Common.Stubs
{
    internal class InnerMiddleware : IRequestMiddleware<CustomRequest, CustomResult>
    {
        private readonly CallChecker _checker;

        public InnerMiddleware(CallChecker checker)
        {
            _checker = checker;
        }

        public async Task<CustomResult> HandleAsync(CustomRequest request, RequestDelegate<Task<CustomResult>> next, CancellationToken cancellationToken)
        {
            _checker.Messages.Add("Inner started");

            CustomResult result = await next();

            _checker.Messages.Add("Inner finished");

            return result;
        }
    }
}
