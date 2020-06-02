using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Requests.Tests.Helpers;

namespace WebFeatures.Requests.Tests.TestObjects
{
    internal class TestRequestHandler : IRequestHandler<TestRequest, TestRequestResult>
    {
        private readonly CallChecker _checker;

        public TestRequestHandler(CallChecker checker)
        {
            _checker = checker;
        }

        public Task<TestRequestResult> HandleAsync(TestRequest request, CancellationToken cancellationToken)
        {
            _checker.Messages.Add("Handler finished");

            return Task.FromResult(new TestRequestResult());
        }
    }
}
