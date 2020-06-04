using System.Data;

namespace WebFeatures.DatabaseInitializer.Core.Database
{
    internal interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
