using System;
using System.Data.Common;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Infrastructure.DataAccess.Factories;

namespace WebFeatures.Infrastructure.DataAccess.Contexts
{
	internal abstract class BaseDbContext : IDbContext, IAsyncDisposable
	{
		private readonly Lazy<DbConnection> _connection;

		protected BaseDbContext(IDbConnectionFactory connectionFactory)
		{
			_connection = new Lazy<DbConnection>(() =>
			{
				DbConnection connection = connectionFactory.CreateConnection();

				connection.Open();

				return connection;
			});
		}

		public async ValueTask DisposeAsync()
		{
			if (_connection.IsValueCreated) await _connection.Value.DisposeAsync();
		}

		public DbConnection Connection => _connection.Value;
	}
}
