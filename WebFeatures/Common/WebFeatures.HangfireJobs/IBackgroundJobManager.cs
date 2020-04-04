using System.Threading.Tasks;

namespace WebFeatures.HangfireJobs
{
    public interface IBackgroundJobManager
    {
        Task EnqueueAsync<TJobArg>(TJobArg argument);
    }
}
