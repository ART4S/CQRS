using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebFeatures.Identity.Model;

namespace WebFeatures.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
