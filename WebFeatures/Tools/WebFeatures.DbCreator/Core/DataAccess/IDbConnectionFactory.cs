using System.Data;

namespace WebFeatures.DbCreator.Core.DataAccess
{
    internal interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
