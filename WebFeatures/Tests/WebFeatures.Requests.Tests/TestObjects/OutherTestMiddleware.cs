using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Requests.Tests.Helpers;

namespace WebFeatures.Requests.Tests.TestObjects
{
    internal class OutherTestMiddleware : IRequestMiddleware<TestRequest, TestRequestResult>
    {
        private readonly CallChecker _checker;

        public OutherTestMiddleware(CallChecker checker)
        {
            _checker = checker;
        }

        public async Task<TestRequestResult> HandleAsync(TestRequest request, RequestDelegate<Task<TestRequestResult>> next, CancellationToken cancellationToken)
        {
            _checker.Messages.Add("Outher started");

            TestRequestResult result = await next();

            _checker.Messages.Add("Outher finished");

            return result;
        }
    }
}
