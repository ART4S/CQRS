using System.Threading.Tasks;

namespace WebFeatures.Application.Interfaces.Jobs
{
    public interface IBackgroundJobManager
    {
        Task EnqueueAsync<TJobArg>(TJobArg argument);
    }
}
