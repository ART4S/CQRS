namespace WebFeatures.Application.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Command handling
    /// </summary>
    /// <typeparam name="TCommand">Command</typeparam>
    /// <typeparam name="TResponse">Command handling result</typeparam>
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
    }
}
