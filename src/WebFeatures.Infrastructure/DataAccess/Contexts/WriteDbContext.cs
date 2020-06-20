using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Permissions;
using WebFeatures.Domian.Entities.Products;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure.DataAccess.Contexts
{
    internal class WriteDbContext : BaseDbContext, IWriteDbContext
    {
        public IUserWriteRepository Users => _users ??= CreateRepository<UserWriteRepository>();
        private IUserWriteRepository _users;

        public IRoleWriteRepository Roles => _roles ??= CreateRepository<RoleWriteRepository>();
        private IRoleWriteRepository _roles;

        public IWriteRepository<UserRole> UserRoles => _userRoles ??= CreateRepository<WriteRepository<UserRole>>();
        private IWriteRepository<UserRole> _userRoles;

        public IWriteRepository<Product> Products => _products ??= CreateRepository<WriteRepository<Product>>();
        private IWriteRepository<Product> _products;

        public IWriteRepository<ProductReview> ProductReviews => _productReviews ??= CreateRepository<WriteRepository<ProductReview>>();
        private IWriteRepository<ProductReview> _productReviews;

        public IWriteRepository<ProductComment> ProductComments => _productComments ??= CreateRepository<WriteRepository<ProductComment>>();
        private IWriteRepository<ProductComment> _productComments;

        public IWriteRepository<ProductPicture> ProductPictures => _productFiles ??= CreateRepository<WriteRepository<ProductPicture>>();
        private IWriteRepository<ProductPicture> _productFiles;

        public IWriteRepository<Manufacturer> Manufacturers => _manufacturers ??= CreateRepository<WriteRepository<Manufacturer>>();
        private IWriteRepository<Manufacturer> _manufacturers;

        public IWriteRepository<Brand> Brands => _brands ??= CreateRepository<WriteRepository<Brand>>();
        private IWriteRepository<Brand> _brands;

        public IWriteRepository<Category> Categories => _categories ??= CreateRepository<WriteRepository<Category>>();
        private IWriteRepository<Category> _categories;

        public IWriteRepository<Shipper> Shippers => _shippers ??= CreateRepository<WriteRepository<Shipper>>();
        private IWriteRepository<Shipper> _shippers;

        public IWriteRepository<City> Cities => _cities ??= CreateRepository<WriteRepository<City>>();
        private IWriteRepository<City> _cities;

        public IWriteRepository<Country> Countries => _countries ??= CreateRepository<WriteRepository<Country>>();
        private IWriteRepository<Country> _countries;

        public IFileWriteRepository Files => _files ??= CreateRepository<FileWriteRepository>();
        private IFileWriteRepository _files;

        protected DbTransaction Transaction { get; private set; }

        private readonly IServiceProvider _services;

        public WriteDbContext(IServiceProvider services) : base(services.GetRequiredService<IDbConnectionFactory>())
        {
            _services = services;
        }

        private TRepo CreateRepository<TRepo>()
        {
            if (Transaction == null)
            {
                Transaction = Connection.BeginTransaction();
            }

            return ActivatorUtilities.CreateInstance<TRepo>(_services, Connection);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            if (Transaction == null) return;

            try
            {
                await Transaction.CommitAsync();
            }
            catch
            {
                await Transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await Transaction.DisposeAsync();

                Transaction = await Connection.BeginTransactionAsync();
            }
        }
    }
}