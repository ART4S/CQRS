using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Jobs;
using WebFeatures.Application.Interfaces.Logging;

namespace WebFeatures.Application.Jobs.Common
{
    public class LoggingBackgroundJob<T> : IBackgroundJob<T>
    {
        private readonly ILogger<T> _logger;
        private readonly IBackgroundJob<T> _decoratee;

        public LoggingBackgroundJob(ILogger<T> logger, IBackgroundJob<T> decoratee)
        {
            _logger = logger;
            _decoratee = decoratee;
        }

        public async Task ExecuteAsync(T argument)
        {
            _logger.LogInformation($"{_decoratee.GetType().Name} started");

            await _decoratee.ExecuteAsync(argument);

            _logger.LogInformation($"{_decoratee.GetType().Name} finished");
        }
    }
}
