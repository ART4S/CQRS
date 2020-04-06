using System.ComponentModel.DataAnnotations;

namespace WebFeatures.WriteContext
{
    public class PostrgeSettings
    {
        [Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; set; }
    }
}
