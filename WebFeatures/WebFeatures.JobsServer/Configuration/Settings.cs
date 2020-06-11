using System.ComponentModel.DataAnnotations;

namespace WebFeatures.JobsServer.Configuration
{
    public class Settings
    {
        [Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; set; }

        public bool EnableDashboard { get; set; }
    }
}