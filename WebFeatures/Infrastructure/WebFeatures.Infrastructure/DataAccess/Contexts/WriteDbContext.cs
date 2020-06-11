using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Permissions;
using WebFeatures.Domian.Entities.Products;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure.DataAccess.Contexts
{
    internal class WriteDbContext : BaseDbContext, IWriteDbContext
    {
        public IUserWriteRepository Users => _users ??= CreateUserRepository();
        private IUserWriteRepository _users;

        public IRoleWriteRepository Roles => _roles ??= CreateRoleRepository();
        private IRoleWriteRepository _roles;

        public IWriteRepository<UserRole> UserRoles => _userRoles ??= CreateRepository<UserRole>();
        private IWriteRepository<UserRole> _userRoles;

        public IWriteRepository<Product> Products => _products ??= CreateRepository<Product>();
        private IWriteRepository<Product> _products;

        public IWriteRepository<ProductReview> ProductReviews => _productReviews ??= CreateRepository<ProductReview>();
        private IWriteRepository<ProductReview> _productReviews;

        public IWriteRepository<ProductComment> ProductComments => _productComments ??= CreateRepository<ProductComment>();
        private IWriteRepository<ProductComment> _productComments;

        public IWriteRepository<ProductPicture> ProductPictures => _productFiles ??= CreateRepository<ProductPicture>();
        private IWriteRepository<ProductPicture> _productFiles;

        public IWriteRepository<Manufacturer> Manufacturers => _manufacturers ??= CreateRepository<Manufacturer>();
        private IWriteRepository<Manufacturer> _manufacturers;

        public IWriteRepository<Brand> Brands => _brands ??= CreateRepository<Brand>();
        private IWriteRepository<Brand> _brands;

        public IWriteRepository<Category> Categories => _categories ??= CreateRepository<Category>();
        private IWriteRepository<Category> _categories;

        public IWriteRepository<Shipper> Shippers => _shippers ??= CreateRepository<Shipper>();
        private IWriteRepository<Shipper> _shippers;

        public IWriteRepository<City> Cities => _cities ??= CreateRepository<City>();
        private IWriteRepository<City> _cities;

        public IWriteRepository<Country> Countries => _countries ??= CreateRepository<Country>();
        private IWriteRepository<Country> _countries;

        public IFileWriteRepository Files => _files ??= CreateFileRepository();
        private IFileWriteRepository _files;

        public IRolePermissionWriteRepository RolePermissions => _rolePermissions ??= CreateRolePermissionRepository();
        private IRolePermissionWriteRepository _rolePermissions;

        private readonly IEntityProfile _profile;

        public WriteDbContext(IDbConnectionFactory connectionFactory, IEntityProfile profile) : base(connectionFactory)
        {
            _profile = profile;
        }

        private IWriteRepository<TEntity> CreateRepository<TEntity>() where TEntity : Entity
            => new WriteRepository<TEntity>(Connection, _profile);

        private IUserWriteRepository CreateUserRepository()
            => new UserWriteRepository(Connection, _profile);

        private IFileWriteRepository CreateFileRepository()
            => new FileWriteRepository(Connection, _profile);

        private IRolePermissionWriteRepository CreateRolePermissionRepository()
            => new RolePermissionWriteRepository(Connection, _profile);

        private IRoleWriteRepository CreateRoleRepository()
            => new RoleWriteRepository(Connection, _profile);
    }
}