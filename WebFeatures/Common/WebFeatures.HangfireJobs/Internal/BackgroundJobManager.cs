using Hangfire;
using System.Threading.Tasks;

namespace WebFeatures.HangfireJobs.Internal
{
    internal class BackgroundJobManager : IBackgroundJobManager
    {
        public Task EnqueueAsync<TJobArg>(TJobArg argument)
        {
            BackgroundJob.Enqueue<IBackgroundJob<TJobArg>>(x => x.ExecuteAsync(argument));

            return Task.CompletedTask;
        }
    }
}
