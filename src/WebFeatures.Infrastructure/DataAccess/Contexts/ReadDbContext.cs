using Microsoft.Extensions.DependencyInjection;
using System;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.DataAccess.Reading.Repositories;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Reading;
using WebFeatures.Infrastructure.DataAccess.Repositories.Reading;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure.DataAccess.Contexts
{
    internal class ReadDbContext : BaseDbContext, IReadDbContext
    {
        public IProductReadRepository Products => _products ??= CreateRepository<ProductReadRepository>();
        private IProductReadRepository _products;

        public IFileReadRepository Files => _files ??= CreateRepository<FileReadRepository>();
        private IFileReadRepository _files;

        private readonly IServiceProvider _services;

        public ReadDbContext(IServiceProvider services) : base(services.GetRequiredService<IDbConnectionFactory>())
        {
            _services = services;
        }

        private TRepo CreateRepository<TRepo>()
            => ActivatorUtilities.CreateInstance<TRepo>(_services, Connection);
    }
}