using System.Data;

namespace WebFeatures.Persistence
{
    internal interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
