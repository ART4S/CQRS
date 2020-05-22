using System;
using System.Data;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.DataAccess.Repositories;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.ValueObjects;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Queries.Builders;
using WebFeatures.Infrastructure.DataAccess.Repositories;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure.DataAccess
{
    internal class DbContext : IDbContext, IDisposable
    {
        private readonly IDbConnection _connection;
        private readonly IEntityProfile _entityProfile;

        public DbContext(IDbConnectionFactory connectionFactory, IEntityProfile entityProfile)
        {
            _connection = connectionFactory.CreateConnection();
            _connection.Open();

            _entityProfile = entityProfile;
        }

        public IUserRepository Users => _users ?? (_users = CreateUserRepository());
        private IUserRepository _users;

        public IAsyncRepository<Role> Roles => _roles ?? (_roles = CreateRepository<Role>());
        private IAsyncRepository<Role> _roles;

        public IAsyncRepository<Product> Products => _products ?? (_products = CreateRepository<Product>());
        private IAsyncRepository<Product> _products;

        public IProductReviewRepository ProductReviews => _productReviews ?? (_productReviews = CreateProductReviewRepository());
        private IProductReviewRepository _productReviews;

        public IProductCommentRepository ProductComments => _productComments ?? (_productComments = CreateProductCommentRepository());
        private IProductCommentRepository _productComments;

        public IAsyncRepository<Manufacturer> Manufacturers => _manufacturers ?? (_manufacturers = CreateRepository<Manufacturer>());
        private IAsyncRepository<Manufacturer> _manufacturers;

        public IAsyncRepository<Brand> Brands => _brands ?? (_brands = CreateRepository<Brand>());
        private IAsyncRepository<Brand> _brands;

        public IAsyncRepository<Category> Categories => _categories ?? (_categories = CreateRepository<Category>());
        private IAsyncRepository<Category> _categories;

        public IAsyncRepository<Shipper> Shippers => _shippers ?? (_shippers = CreateRepository<Shipper>());
        private IAsyncRepository<Shipper> _shippers;

        public IAsyncRepository<City> Cities => _cities ?? (_cities = CreateRepository<City>());
        private IAsyncRepository<City> _cities;

        public IAsyncRepository<Country> Countries => _countries ?? (_countries = CreateRepository<Country>());
        private IAsyncRepository<Country> _countries;

        public IAsyncRepository<File> Files => _files ?? (_files = CreateRepository<File>());
        private IAsyncRepository<File> _files;

        public IAsyncRepository<Address> Addresses => _addresses ?? (_addresses = CreateRepository<Address>());
        private IAsyncRepository<Address> _addresses;

        private IAsyncRepository<TEntity> CreateRepository<TEntity>() where TEntity : Entity
        {
            return new Repository<TEntity, QueryBuilder<TEntity>>(
                _connection,
                new QueryBuilder<TEntity>(_entityProfile));
        }

        private IUserRepository CreateUserRepository()
        {
            return new UserRepository(
                _connection,
                new UserQueryBuilder(_entityProfile));
        }

        private IProductCommentRepository CreateProductCommentRepository()
        {
            return new ProductCommentRepository(
                _connection,
                new ProductCommentQueryBuilder(_entityProfile));
        }

        private IProductReviewRepository CreateProductReviewRepository()
        {
            return new ProductReviewRepository(
                _connection,
                new ProductReviewQueryBuilder(_entityProfile));
        }

        public IDbTransaction BeginTransaction()
        {
            return _connection.BeginTransaction();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
