using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebFeatures.Application.Interfaces.Data;
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

        public T GetById<T, TId>(TId id) where T : class, IEntity<TId>, new() where TId : struct, IEquatable<TId>
            => Find<T>(new T() { Id = id });

        public void Add<T, TId>(T entity) where T : class, IEntity<TId>, new() where TId : struct, IEquatable<TId>
            => Set<T>().Add(entity);

        public void Remove<T, TId>(TId id) where T : class, IEntity<TId>, new() where TId : struct, IEquatable<TId>
            => Remove(new T() { Id = id });

        public bool Exists<T, TId>(TId id) where T : class, IEntity<TId>, new() where TId : struct, IEquatable<TId>
            => Set<T>().AsNoTracking().Any(x => x.Id.Equals(id));

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
