using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebFeaturesDbContext).Assembly);

            SetUtcConverters(modelBuilder);
        }

        private void SetUtcConverters(ModelBuilder modelBuilder)
        {
            var utcConverter = new ValueConverter<DateTime, DateTime>(
                to => to,
                from => DateTime.SpecifyKind(from, DateTimeKind.Utc));

            var nullableUtcConverter = new ValueConverter<DateTime?, DateTime?>(
                to => to,
                from => from != default
                    ? DateTime.SpecifyKind(from.Value, DateTimeKind.Utc)
                    : default);

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

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleRelation> UserRoleRelations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
