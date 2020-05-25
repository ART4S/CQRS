using System;
using System.Data;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.DataAccess.Repositories;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Queries.Builders;
using WebFeatures.Infrastructure.DataAccess.Repositories;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure.DataAccess
{
    internal class DbContext : IDbContext, IDisposable
    {
        public IUserRepository Users => _users ??= CreateUserRepository();
        private IUserRepository _users;

        public IAsyncRepository<Role> Roles => _roles ??= CreateRepository<Role>();
        private IAsyncRepository<Role> _roles;

        public IAsyncRepository<Product> Products => _products ??= CreateRepository<Product>();
        private IAsyncRepository<Product> _products;

        public IProductReviewRepository ProductReviews => _productReviews ??= CreateProductReviewRepository();
        private IProductReviewRepository _productReviews;

        public IProductCommentRepository ProductComments => _productComments ??= CreateProductCommentRepository();
        private IProductCommentRepository _productComments;

        public IAsyncRepository<Manufacturer> Manufacturers => _manufacturers ??= CreateRepository<Manufacturer>();
        private IAsyncRepository<Manufacturer> _manufacturers;

        public IAsyncRepository<Brand> Brands => _brands ??= CreateRepository<Brand>();
        private IAsyncRepository<Brand> _brands;

        public IAsyncRepository<Category> Categories => _categories ??= CreateRepository<Category>();
        private IAsyncRepository<Category> _categories;

        public IAsyncRepository<Shipper> Shippers => _shippers ??= CreateRepository<Shipper>();
        private IAsyncRepository<Shipper> _shippers;

        public IAsyncRepository<City> Cities => _cities ??= CreateRepository<City>();
        private IAsyncRepository<City> _cities;

        public IAsyncRepository<Country> Countries => _countries ??= CreateRepository<Country>();
        private IAsyncRepository<Country> _countries;

        public IAsyncRepository<File> Files => _files ??= CreateRepository<File>();
        private IAsyncRepository<File> _files;

        private IDbConnection Connection => _connection.Value;
        private Lazy<IDbConnection> _connection;

        private readonly IEntityProfile _entityProfile;

        public DbContext(IDbConnectionFactory connectionFactory, IEntityProfile entityProfile)
        {
            _connection = new Lazy<IDbConnection>(() =>
            {
                IDbConnection connection = connectionFactory.CreateConnection();
                connection.Open();

                return connection;
            });

            _entityProfile = entityProfile;
        }

        private IAsyncRepository<TEntity> CreateRepository<TEntity>() where TEntity : IdentityEntity
        {
            return new Repository<TEntity, QueryBuilder<TEntity>>(
                Connection,
                new QueryBuilder<TEntity>(_entityProfile));
        }

        private IUserRepository CreateUserRepository()
        {
            return new UserRepository(
                Connection,
                new UserQueryBuilder(_entityProfile));
        }

        private IProductCommentRepository CreateProductCommentRepository()
        {
            return new ProductCommentRepository(
                Connection,
                new ProductCommentQueryBuilder(_entityProfile));
        }

        private IProductReviewRepository CreateProductReviewRepository()
        {
            return new ProductReviewRepository(
                Connection,
                new ProductReviewQueryBuilder(_entityProfile));
        }

        public IDbTransaction BeginTransaction()
        {
            return Connection.BeginTransaction();
        }

        public void Dispose()
        {
            if (_connection.IsValueCreated)
            {
                _connection.Value.Dispose();
            }
        }
    }
}
