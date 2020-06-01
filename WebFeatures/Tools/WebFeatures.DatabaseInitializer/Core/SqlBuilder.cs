using System.IO;

namespace WebFeatures.DatabaseInitializer.Core
{
    internal static class SqlBuilder
    {
        public static string CreateDatabase(string databaseName)
        {
            string closeExistingConnections =
                "SELECT pg_terminate_backend(pid)\n" +
                "FROM pg_stat_activity\n" +
                $"WHERE datname = '{databaseName}' AND pid <> pg_backend_pid();\n";

            string dropDatabase =
                $"DROP DATABASE IF EXISTS {databaseName};\n";

            string createDatabase =
                $"CREATE DATABASE {databaseName};\n";

            return closeExistingConnections + dropDatabase + createDatabase;
        }

        public static string CreateDbSchema()
        {
            string createTables =
                $"{File.ReadAllText("Scripts/Schema/Tables.sql")};\n";

            string createFunctions =
                $"{File.ReadAllText("Scripts/Schema/Functions.sql")};\n";

            return createTables + createFunctions;
        }

        public static string SeedInitialData()
        {
            string insertData =
                File.ReadAllText("Scripts/InitialData.sql");

            return insertData;
        }
    }
}
