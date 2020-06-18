using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Infrastructure.Tests.Common.Utils;

namespace WebFeatures.Infrastructure.Tests.Common.Stubs
{
    internal class TestRequestHandler : IRequestHandler<TestRequest, TestResult>
    {
        private readonly CallChecker _checker;

        public TestRequestHandler(CallChecker checker)
        {
            _checker = checker;
        }

        public Task<TestResult> HandleAsync(TestRequest request, CancellationToken cancellationToken)
        {
            _checker.Messages.Add("Handler finished");

            return Task.FromResult(new TestResult());
        }
    }
}
