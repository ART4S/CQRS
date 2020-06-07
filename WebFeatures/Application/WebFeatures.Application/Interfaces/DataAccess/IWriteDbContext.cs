using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Interfaces.DataAccess
{
    public interface IWriteDbContext : IDbContext
    {
        IUserWriteRepository Users { get; }
        IWriteRepository<Role> Roles { get; }
        IWriteRepository<UserRole> UserRoles { get; }
        IWriteRepository<File> Files { get; }
        IWriteRepository<Product> Products { get; }
        IWriteRepository<ProductFile> ProductFiles { get; }
        IWriteRepository<ProductReview> ProductReviews { get; }
        IWriteRepository<ProductComment> ProductComments { get; }
        IWriteRepository<Manufacturer> Manufacturers { get; }
        IWriteRepository<Brand> Brands { get; }
        IWriteRepository<Category> Categories { get; }
        IWriteRepository<Shipper> Shippers { get; }
        IWriteRepository<City> Cities { get; }
        IWriteRepository<Country> Countries { get; }
    }
}