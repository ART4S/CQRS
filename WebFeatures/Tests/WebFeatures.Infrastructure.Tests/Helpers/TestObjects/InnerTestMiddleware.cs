using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Requests.Tests.Helpers;

namespace WebFeatures.Infrastructure.Tests.Helpers.TestObjects
{
    internal class InnerTestMiddleware : IRequestMiddleware<TestRequest, TestRequestResult>
    {
        private readonly CallChecker _checker;

        public InnerTestMiddleware(CallChecker checker)
        {
            _checker = checker;
        }

        public async Task<TestRequestResult> HandleAsync(TestRequest request, RequestDelegate<Task<TestRequestResult>> next, CancellationToken cancellationToken)
        {
            _checker.Messages.Add("Inner started");

            TestRequestResult result = await next();

            _checker.Messages.Add("Inner finished");

            return result;
        }
    }
}
