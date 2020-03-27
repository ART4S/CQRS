using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Events;
using WebFeatures.Events;

namespace WebFeatures.DataContext
{
    public class WebFeaturesDbContext : DbContext, IWebFeaturesDbContext
    {
        private readonly IEventMediator _eventMediator;

        public WebFeaturesDbContext(
            DbContextOptions<WebFeaturesDbContext> options,
            IEventMediator eventMediator) : base(options)
        {
            _eventMediator = eventMediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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
        public DbSet<File> Files { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            int savedCount = await base.SaveChangesAsync();

            IEnumerable<Task> events = ChangeTracker.Entries<IHasDomianEvents>()
                .SelectMany(x => x.Entity.Events)
                .Select(x => _eventMediator.PublishAsync(x));

            await Task.WhenAll(events);

            return savedCount;
        }
    }
}
