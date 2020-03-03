using Microsoft.EntityFrameworkCore;

namespace WebFeatures.DataContext
{
    public class WebFeaturesDbContext : DbContext
    {
        public WebFeaturesDbContext(DbContextOptions<WebFeaturesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebFeaturesDbContext).Assembly);
        }
    }
}
