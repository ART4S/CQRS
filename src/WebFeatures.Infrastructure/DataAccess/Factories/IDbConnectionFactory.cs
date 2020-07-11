using System.Data.Common;

namespace WebFeatures.Infrastructure.DataAccess.Factories
{
    internal interface IDbConnectionFactory
    {
        DbConnection CreateConnection();
    }
}
