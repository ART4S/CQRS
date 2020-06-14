namespace WebFeatures.Application.Infrastructure.Requests
{
    public interface IRequest<TResponse> { }

    internal interface IQuery<TResponse> : IRequest<TResponse> { }

    internal interface ICommand<TResponse> : IRequest<TResponse> { }
}
