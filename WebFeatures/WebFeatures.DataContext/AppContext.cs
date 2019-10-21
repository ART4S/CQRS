using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebFeatures.Application.Interfaces;
using WebFeatures.Common.Time;
using WebFeatures.Domian.Entities.Abstractions;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.DataContext
{
    public abstract class AppContext : DbContext, IAppContext
    {
        protected AppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<ContactDetails> ContactDetailses { get; set; }

        public T GetById<T>(int id) where T : class, IEntity, new()
            => Find<T>(new T() { Id = id });

        public new void Add<T>(T entity) where T : class, IEntity, new()
            => Set<T>().Add(entity);

        public void Remove<T>(int id) where T : class, IEntity, new()
            => Remove(new T() { Id = id });

        public bool Exists<T>(int id) where T : class, IEntity, new()
            => Set<T>().AsNoTracking().Any(x => x.Id == id);

        public override int SaveChanges()
        {
            var now = DateTimeProvider.Instance.Now;

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
