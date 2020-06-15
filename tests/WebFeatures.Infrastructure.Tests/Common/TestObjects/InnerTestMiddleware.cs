using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Infrastructure.Tests.Common.Utils;

namespace WebFeatures.Infrastructure.Tests.Common.TestObjects
{
    internal class InnerTestMiddleware : IRequestMiddleware<TestRequest, TestResult>
    {
        private readonly CallChecker _checker;

        public InnerTestMiddleware(CallChecker checker)
        {
            _checker = checker;
        }

        public async Task<TestResult> HandleAsync(TestRequest request, RequestDelegate<Task<TestResult>> next, CancellationToken cancellationToken)
        {
            _checker.Messages.Add("Inner started");

            TestResult result = await next();

            _checker.Messages.Add("Inner finished");

            return result;
        }
    }
}
