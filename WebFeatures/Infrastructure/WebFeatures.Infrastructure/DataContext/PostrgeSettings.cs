using System.ComponentModel.DataAnnotations;

namespace WebFeatures.DataContext
{
    public class PostrgeSettings
    {
        [Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; set; }
    }
}
