using System.Data;

namespace WebFeatures.AppInitializer.Extensions
{
    internal static class DbConnectionExtensions
    {
        public static void ExecuteScript(this IDbConnection connection, string script)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = script;
            command.ExecuteNonQuery();
        }
    }
}
