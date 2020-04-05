using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Common;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Entities;
using WebFeatures.Events;

namespace WebFeatures.WriteContext
{
    public class EFWriteContext : DbContext, IWriteContext
    {
        private readonly IEventMediator _eventMediator;
        private readonly IDateTime _dateTime;

        public EFWriteContext(
            DbContextOptions<EFWriteContext> options,
            IEventMediator eventMediator,
            IDateTime dateTime) : base(options)
        {
            _eventMediator = eventMediator;
            _dateTime = dateTime;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyTableNames();
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
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserComment> UserComments { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IHasCreateDate cd)
                {
                    cd.CreateDate = _dateTime.Now;
                }
            }

            int savedCount = await base.SaveChangesAsync();

            IEnumerable<Task> events = ChangeTracker.Entries<BaseEntity>()
                .SelectMany(x => x.Entity.Events)
                .Select(x => _eventMediator.PublishAsync(x));

            await Task.WhenAll(events);

            return savedCount;
        }
    }
}
