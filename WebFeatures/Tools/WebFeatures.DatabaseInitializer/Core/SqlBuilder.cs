using System.IO;
using System.Text;

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
                $"{File.ReadAllText("Core/Scripts/Schema/Tables.sql")};\n";

            string createFunctions =
                $"{File.ReadAllText("Core/Scripts/Schema/Functions.sql")};\n";

            return createTables + createFunctions;
        }

        public static string SeedInitialData()
        {
            return File.ReadAllText("Core/Scripts/InitialData.sql");
        }

        public static string RefreshMaterializedViews()
        {
            string[] views =
            {
                "get_products_list",
                "get_product_comments"
            };

            var sb = new StringBuilder();

            foreach (string view in views)
            {
                sb.AppendLine($"REFRESH MATERIALIZED VIEW {view};");
            }

            return sb.ToString();
        }
    }
}
