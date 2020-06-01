using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Requests.Tests.TestObjects
{
    internal class TestRequest : IRequest<TestResponse>
    {
    }

    internal class TestRequestHandler : IRequestHandler<TestRequest, TestResponse>
    {
        public Task<TestResponse> HandleAsync(TestRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class TestResponse
    {
        public int Status { get; }
    }
}
