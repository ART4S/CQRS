using System.ComponentModel.DataAnnotations;

namespace WebFeatures.ReadContext
{
    public class MongoDbSettings
    {
        [Required(AllowEmptyStrings = false)]
        public string DatabaseName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; set; }
    }
}
