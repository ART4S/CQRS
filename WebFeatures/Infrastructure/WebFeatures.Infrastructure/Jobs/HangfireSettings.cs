using System.ComponentModel.DataAnnotations;

namespace WebFeatures.Infrastructure.HangfireJobs
{
    public class HangfireSettings
    {
        [Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; set; }

        public bool EnableDashboard { get; set; }
    }
}
