using System.ComponentModel.DataAnnotations;

namespace WebFeatures.ReadContext
{
    public class MongoSettings
    {
        [Required(AllowEmptyStrings = false)]
        public string DatabaseName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; set; }
    }
}
