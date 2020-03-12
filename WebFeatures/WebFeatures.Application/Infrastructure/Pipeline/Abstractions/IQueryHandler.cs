namespace WebFeatures.Application.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Query handling
    /// </summary>
    /// <typeparam name="TQuery">Query</typeparam>
    /// <typeparam name="TResponse">Query handling result</typeparam>
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
    }
}
