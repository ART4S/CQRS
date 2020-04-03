using WebFeatures.Requests;

namespace WebFeatures.Application.Infrastructure.Requests
{
    internal interface ICommand<TResult> : IRequest<TResult> { }
}
