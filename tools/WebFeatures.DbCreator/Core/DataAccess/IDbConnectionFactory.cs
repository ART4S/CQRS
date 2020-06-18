using System.Data.Common;

namespace WebFeatures.DbCreator.Core.DataAccess
{
    internal interface IDbConnectionFactory
    {
        DbConnection CreateConnection();
    }
}
