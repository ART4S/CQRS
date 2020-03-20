using WebFeatures.Requests;

namespace WebFeatures.Application.Infrastructure.Requests
{
    public interface ICommand<TResult> : IRequest<TResult> { }
}
