using System.Data;
using WebFeatures.Infrastructure.DataAccess.QueryExecutors;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Reading
{
    internal class ReadRepository
    {
        protected IDbConnection Connection { get; }
        protected IDbExecutor Executor { get; }

        public ReadRepository(IDbConnection connection, IDbExecutor executor)
        {
            Connection = connection;
            Executor = executor;
        }
    }
}