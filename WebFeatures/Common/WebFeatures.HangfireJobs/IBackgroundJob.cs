using System.Threading.Tasks;

namespace WebFeatures.HangfireJobs
{
    public interface IBackgroundJob<in TJobArg>
    {
        Task ExecuteAsync(TJobArg argument);
    }
}
