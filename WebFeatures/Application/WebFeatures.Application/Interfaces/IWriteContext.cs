using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Interfaces
{
    public interface IWriteContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Manufacturer> Manufacturers { get; set; }
        DbSet<Brand> Brands { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Shipper> Shippers { get; set; }
        DbSet<File> Files { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<UserComment> UserComments { get; set; }

        Task<int> SaveChangesAsync();
    }
}
