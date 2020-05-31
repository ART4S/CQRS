using System.IO;

namespace WebFeatures.DatabaseInitializer
{
    internal static class SqlBuilder
    {
        public static string CreateDatabase(string databaseName)
        {
            string query =
                "SELECT pg_terminate_backend(pid)\n" +
                "FROM pg_stat_activity\n" +
                $"WHERE datname = '{databaseName}' AND pid <> pg_backend_pid();\n" +
                $"DROP DATABASE IF EXISTS {databaseName};\n" +
                $"CREATE DATABASE {databaseName};\n";

            return query;
        }

        public static string CreateDbSchema()
        {
            string query =
                  $"{File.ReadAllText("Scripts/Schema/Tables.sql")};\n" +
                  $"{File.ReadAllText("Scripts/Schema/Functions.sql")};\n";

            return query;
        }

        public static string SeedInitialData()
            => File.ReadAllText("Scripts/InitialData.sql");
    }
}
