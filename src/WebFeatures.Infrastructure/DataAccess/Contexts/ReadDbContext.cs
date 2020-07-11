using System;
using Microsoft.Extensions.DependencyInjection;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Reading;
using WebFeatures.Infrastructure.DataAccess.Factories;
using WebFeatures.Infrastructure.DataAccess.Repositories.Reading;

namespace WebFeatures.Infrastructure.DataAccess.Contexts
{
	internal class ReadDbContext : BaseDbContext, IReadDbContext
	{
		private readonly IServiceProvider _services;

		public ReadDbContext(IServiceProvider services) : base(services.GetRequiredService<IDbConnectionFactory>())
		{
			_services = services;
		}

		public IProductReadRepository Products => _products ??= CreateRepository<ProductReadRepository>();
		private IProductReadRepository _products;

		public IFileReadRepository Files => _files ??= CreateRepository<FileReadRepository>();
		private IFileReadRepository _files;

		private TRepo CreateRepository<TRepo>()
		{
			return ActivatorUtilities.CreateInstance<TRepo>(_services, Connection);
		}
	}
}
