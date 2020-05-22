using System.IO;

namespace WebFeatures.Infrastructure.Tests.Helpers
{
    internal static class ScriptBuilder
    {
        public static string DropDatabase(string databaseName)
        {
            return $"DROP DATABASE IF EXISTS {databaseName};";
        }

        public static string CreateDatabase(string databaseName)
        {
            return $"CREATE DATABASE {databaseName};";
        }

        public static string CloseExistingConnections(string databaseName)
        {
            return
                "SELECT pg_terminate_backend(pid)\n" +
                "FROM pg_stat_activity\n" +
                $"WHERE datname = '{databaseName}' AND pid <> pg_backend_pid();\n";
        }

        public static string CreateSchema()
        {
            return File.ReadAllText("Scripts/Schema.sql");
        }
    }
}