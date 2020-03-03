using Microsoft.AspNetCore.Identity;

namespace WebFeatures.Identity.Model
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
