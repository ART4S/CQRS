using Dapper;
using System.Data;

namespace WebFeatures.Infrastructure.Tests.Helpers
{
    internal static class ConnectionExtensions
    {
        public static void Execute(this IDbConnection connection, SqlQuery sql)
        {
            connection.Execute(sql.Query, sql.Param);
        }
    }
}
