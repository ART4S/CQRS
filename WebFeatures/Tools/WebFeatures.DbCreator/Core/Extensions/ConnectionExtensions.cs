using System.Data;

namespace WebFeatures.DbCreator.Core.Extensions
{
    internal static class ConnectionExtensions
    {
        public static void Execute(this IDbConnection connection, string script)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = script;
            command.ExecuteNonQuery();
        }
    }
}
