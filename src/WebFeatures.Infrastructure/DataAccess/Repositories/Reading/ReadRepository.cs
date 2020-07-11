using System.Data;
using WebFeatures.Infrastructure.DataAccess.Executors;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Reading
{
	internal class ReadRepository
	{
		public ReadRepository(IDbConnection connection, IDbExecutor executor)
		{
			Connection = connection;
			Executor = executor;
		}

		protected IDbConnection Connection { get; }
		protected IDbExecutor Executor { get; }
	}
}
