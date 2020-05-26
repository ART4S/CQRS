using System.Data;

namespace WebFeatures.DatabaseInitializer.Database
{
    internal interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
