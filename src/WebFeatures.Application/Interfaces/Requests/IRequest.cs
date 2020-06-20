namespace WebFeatures.Application.Interfaces.Requests
{
    public interface IRequest<TResponse> { }

    internal interface IQuery<TResponse> : IRequest<TResponse> { }

    internal interface ICommand<TResponse> : IRequest<TResponse> { }
}
