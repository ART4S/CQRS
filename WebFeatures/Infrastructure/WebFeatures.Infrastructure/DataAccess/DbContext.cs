using System;
using System.Data;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.DataAccess.Repositories;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Mappings.QueryBuilders;
using WebFeatures.Infrastructure.DataAccess.Mappings.Querying;
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
            _entityProfile = entityProfile;
        }

        public IUserRepository Users => _users ?? (_users = new UserRepository(_connection, new UserQueryBuilder(_entityProfile).BuildQueries()));
        private IUserRepository _users;

        public IAsyncRepository<Role> Roles => _roles ?? (_roles = new Repository<Role, Queries>(_connection, new QueryBuilder<Role, Queries>(_entityProfile).BuildQueries()));
        private IAsyncRepository<Role> _roles;

        public IAsyncRepository<Product> Products => _products ?? (_products = new Repository<Product, Queries>(_connection, new QueryBuilder<Product, Queries>(_entityProfile).BuildQueries()));
        private IAsyncRepository<Product> _products;

        public IProductReviewRepository ProductReviews => _productReviews ?? (_productReviews = new ProductReviewRepository(_connection, new ProductReviewQueryBuilder(_entityProfile).BuildQueries()));
        private IProductReviewRepository _productReviews;

        public IProductCommentRepository ProductComments => _productComments ?? (_productComments = new ProductCommentRepository(_connection, new ProductCommentQueryBuilder(_entityProfile).BuildQueries()));
        private IProductCommentRepository _productComments;

        public IAsyncRepository<Manufacturer> Manufacturers => _manufacturers ?? (_manufacturers = new Repository<Manufacturer, Queries>(_connection, new QueryBuilder<Manufacturer, Queries>(_entityProfile).BuildQueries()));
        private IAsyncRepository<Manufacturer> _manufacturers;

        public IAsyncRepository<Brand> Brands => _brands ?? (_brands = new Repository<Brand, Queries>(_connection, new QueryBuilder<Brand, Queries>(_entityProfile).BuildQueries()));
        private IAsyncRepository<Brand> _brands;

        public IAsyncRepository<Category> Categories => _categories ?? (_categories = new Repository<Category, Queries>(_connection, new QueryBuilder<Category, Queries>(_entityProfile).BuildQueries()));
        private IAsyncRepository<Category> _categories;

        public IAsyncRepository<Shipper> Shippers => _shippers ?? (_shippers = new Repository<Shipper, Queries>(_connection, new QueryBuilder<Shipper, Queries>(_entityProfile).BuildQueries()));
        private IAsyncRepository<Shipper> _shippers;

        public IAsyncRepository<City> Cities => _cities ?? (_cities = new Repository<City, Queries>(_connection, new QueryBuilder<City, Queries>(_entityProfile).BuildQueries()));
        private IAsyncRepository<City> _cities;

        public IAsyncRepository<Country> Countries => _countries ?? (_countries = new Repository<Country, Queries>(_connection, new QueryBuilder<Country, Queries>(_entityProfile).BuildQueries()));
        private IAsyncRepository<Country> _countries;

        public IAsyncRepository<File> Files => _files ?? (_files = new Repository<File, Queries>(_connection, new QueryBuilder<File, Queries>(_entityProfile).BuildQueries()));
        private IAsyncRepository<File> _files;

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
