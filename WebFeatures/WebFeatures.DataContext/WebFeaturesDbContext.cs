using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Common;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Events;
using WebFeatures.Events;

namespace WebFeatures.DataContext
{
    public class WebFeaturesDbContext : DbContext, IWebFeaturesDbContext
    {
        private readonly IDateTime _dateTime;
        private readonly IEventMediator _eventMediator;

        public WebFeaturesDbContext(
            DbContextOptions<WebFeaturesDbContext> options,
            IDateTime dateTime,
            IEventMediator eventMediator) : base(options)
        {
            _dateTime = dateTime;
            _eventMediator = eventMediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            SetupUtcConverters(modelBuilder);
            SetupSoftDelete(modelBuilder);

            modelBuilder.Ignore<DomianEvent>();
        }

        private void SetupUtcConverters(ModelBuilder modelBuilder)
        {
            var utcConverter = new ValueConverter<DateTime, DateTime>(
                to => to,
                from => DateTime.SpecifyKind(from, DateTimeKind.Utc));

            var nullableUtcConverter = new ValueConverter<DateTime?, DateTime?>(
                to => to,
                from => from != default ? DateTime.SpecifyKind(from.Value, DateTimeKind.Utc) : default);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.Name.EndsWith("Utc"))
                    {
                        if (property.ClrType == typeof(DateTime))
                        {
                            property.SetValueConverter(utcConverter);
                        }

                        if (property.ClrType == typeof(DateTime?))
                        {
                            property.SetValueConverter(nullableUtcConverter);
                        }
                    }
                }
            }
        }

        private void SetupSoftDelete(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    typeof(WebFeaturesDbContext)
                        .GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Instance)
                        .MakeGenericMethod(entityType.ClrType)
                        .Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        private void SetSoftDeleteFilter<T>(ModelBuilder modelBuilder) where T : class, ISoftDelete
        {
            modelBuilder.Entity<T>().HasQueryFilter(x => !x.IsDeleted);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Shipper> Shippers { get; set; }

        public Task<IDbContextTransaction> BeginTransaction()
        {
            return Database.BeginTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            var now = _dateTime.Now;

            foreach (EntityEntry<IUpdatable> entry in ChangeTracker.Entries<IUpdatable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = now;
                        break;
                }
            }

            foreach (EntityEntry<ISoftDelete> entry in ChangeTracker.Entries<ISoftDelete>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            }

            int result = await base.SaveChangesAsync();

            IEnumerable<Task> events = ChangeTracker.Entries<BaseEntity>()
                .SelectMany(x => x.Entity.EventsList)
                .Select(x => _eventMediator.PublishAsync(x));

            await Task.WhenAll(events);

            return result;
        }
    }
}
