using WebFeatures.Requests;

namespace WebFeatures.Application.Infrastructure.Requests
{
    internal interface IQuery<TResult> : IRequest<TResult> { }
}
