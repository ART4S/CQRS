using System.Threading.Tasks;

namespace WebFeatures.Application.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Mediator for sending request to apropriate handlers
    /// </summary>
    public interface IRequestMediator
    {
        /// <summary>
        /// Send query to apropriate query handler
        /// </summary>
        /// <typeparam name="TResponse">Query handling result</typeparam>
        /// <param name="query">Input query</param>
        /// <returns></returns>
        Task<TResponse> SendQueryAsync<TResponse>(IQuery<TResponse> query);

        /// <summary>
        /// Send command to apropriate command handler
        /// </summary>
        /// <typeparam name="TResponse">Command handlging result</typeparam>
        /// <param name="command">Input command</param>
        /// <returns></returns>
        Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command);
    }
}
