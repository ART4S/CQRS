namespace WebFeatures.Application.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Query handling
    /// </summary>
    /// <typeparam name="TQuery">Query</typeparam>
    /// <typeparam name="TResponse">Query handling result</typeparam>
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
    }
}
