using System.ComponentModel.DataAnnotations;

namespace WebFeatures.Infrastructure.HangfireJobs
{
    public class HangfireJobsSettings
    {
        [Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; }
    }
}
