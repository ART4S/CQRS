using System.Data;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Reading
{
    internal class ReadRepository
    {
        protected IDbConnection Connection { get; }

        public ReadRepository(IDbConnection connection)
        {
            Connection = connection;
        }
    }
}
