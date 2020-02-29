using Microsoft.EntityFrameworkCore;
using WebFeatures.Common.Time;
using WebFeatures.Domian.Entities.Abstractions;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.DataContext
{
    public class AppContext : DbContext
    {
        private readonly IDateTime _dateTime;

        public AppContext(
            DbContextOptions options, 
            IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<ContactDetails> ContactDetailes { get; set; }

        public override int SaveChanges()
        {
            var now = _dateTime.Now;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IUpdatable updatable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            updatable.CreatedAt = now;
                            break;

                        case EntityState.Modified:
                            updatable.UpdatedAt = now;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
