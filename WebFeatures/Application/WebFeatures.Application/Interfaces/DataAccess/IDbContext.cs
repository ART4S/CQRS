using WebFeatures.Application.Interfaces.DataAccess.Repositories;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Interfaces.DataAccess
{
    public interface IDbContext
    {
        IUserRepository Users { get; }
        IAsyncRepository<Role> Roles { get; }
        IAsyncRepository<Product> Products { get; }
        IProductReviewRepository ProductReviews { get; }
        IProductCommentRepository ProductComments { get; }
        IAsyncRepository<Manufacturer> Manufacturers { get; }
        IAsyncRepository<Brand> Brands { get; }
        IAsyncRepository<Category> Categories { get; }
        IAsyncRepository<Shipper> Shippers { get; }
        IAsyncRepository<City> Cities { get; }
        IAsyncRepository<Country> Countries { get; }
        IAsyncRepository<File> Files { get; }

        int SaveChanges();
    }
}
