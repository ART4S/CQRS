using Hangfire;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Jobs;

namespace WebFeatures.Infrastructure.Jobs
{
    internal class BackgroundJobManager : IBackgroundJobManager
    {
        private readonly IBackgroundJobClient _jobClient;

        public BackgroundJobManager(IBackgroundJobClient jobClient)
        {
            _jobClient = jobClient;
        }

        public Task EnqueueAsync<TJobArg>(TJobArg argument)
        {
            _jobClient.Enqueue<IBackgroundJob<TJobArg>>(x => x.ExecuteAsync(argument));

            return Task.CompletedTask;
        }
    }
}
