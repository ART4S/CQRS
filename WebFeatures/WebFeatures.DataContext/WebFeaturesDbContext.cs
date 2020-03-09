using Microsoft.EntityFrameworkCore;
using WebFeatures.Domian.Model;

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

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleRelation> UserRoleRelations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
