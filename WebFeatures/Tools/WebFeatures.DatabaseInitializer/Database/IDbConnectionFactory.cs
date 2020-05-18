using System.Data;

namespace WebFeatures.AppInitializer.Database
{
    internal interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
