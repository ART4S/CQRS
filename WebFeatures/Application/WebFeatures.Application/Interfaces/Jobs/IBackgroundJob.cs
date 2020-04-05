using System.Threading.Tasks;

namespace WebFeatures.Application.Interfaces.Jobs
{
    public interface IBackgroundJob<in TJobArg>
    {
        Task ExecuteAsync(TJobArg argument);
    }
}
