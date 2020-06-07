using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Queries.Builders;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure.DataAccess.Contexts
{
    internal class WriteDbContext : BaseDbContext, IWriteDbContext
    {
        public IUserWriteRepository Users => _users ??= CreateUserRepository();
        private IUserWriteRepository _users;

        public IWriteRepository<Role> Roles => _roles ??= CreateRepository<Role>();
        private IWriteRepository<Role> _roles;

        public IWriteRepository<UserRole> UserRoles => _userRoles ??= CreateRepository<UserRole>();
        private IWriteRepository<UserRole> _userRoles;

        public IWriteRepository<Product> Products => _products ??= CreateRepository<Product>();
        private IWriteRepository<Product> _products;

        public IWriteRepository<ProductReview> ProductReviews => _productReviews ??= CreateRepository<ProductReview>();
        private IWriteRepository<ProductReview> _productReviews;

        public IWriteRepository<ProductComment> ProductComments => _productComments ??= CreateRepository<ProductComment>();
        private IWriteRepository<ProductComment> _productComments;

        public IWriteRepository<ProductFile> ProductFiles => _productFiles ??= CreateRepository<ProductFile>();
        private IWriteRepository<ProductFile> _productFiles;

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

        public IWriteRepository<File> Files => _files ??= CreateRepository<File>();
        private IWriteRepository<File> _files;

        private readonly IEntityProfile _entityProfile;

        public WriteDbContext(
            IDbConnectionFactory connectionFactory,
            IEntityProfile entityProfile) : base(connectionFactory)
        {
            _entityProfile = entityProfile;
        }

        private IWriteRepository<TEntity> CreateRepository<TEntity>() where TEntity : Entity
        {
            return new WriteRepository<TEntity, QueryBuilder<TEntity>>(
                Connection,
                new QueryBuilder<TEntity>(_entityProfile));
        }

        private IUserWriteRepository CreateUserRepository()
        {
            return new UserWriteRepository(Connection, new UserQueryBuilder(_entityProfile));
        }
    }
}
