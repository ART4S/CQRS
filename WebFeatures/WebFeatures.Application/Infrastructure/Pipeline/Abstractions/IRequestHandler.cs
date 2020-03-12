using System.Threading.Tasks;

namespace WebFeatures.Application.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Request handling
    /// </summary>
    /// <typeparam name="TRequest">Request</typeparam>
    /// <typeparam name="TResponse">Request handling result</typeparam>
    public interface IRequestHandler<TRequest, TResponse>
    {
        /// <summary>
        /// Handle request
        /// </summary>
        /// <param name="request">Input request</param>
        Task<TResponse> HandleAsync(TRequest request);
    }
}
