using WebFeatures.Requests;

namespace WebFeatures.Application.Infrastructure.Requests
{
    public interface IQuery<TResult> : IRequest<TResult> { }
}
