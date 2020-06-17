using System.Data.Common;

namespace WebFeatures.Persistence
{
    internal interface IDbConnectionFactory
    {
        DbConnection CreateConnection();
    }
}
