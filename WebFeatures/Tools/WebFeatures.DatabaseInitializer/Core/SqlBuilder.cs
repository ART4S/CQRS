using System.IO;
using System.Linq;
using System.Text;

namespace WebFeatures.DatabaseInitializer.Core
{
    internal static class SqlBuilder
    {
        public static string CreateDatabase(string databaseName)
        {
            var sb = new StringBuilder();

            sb.AppendLine(
                @$"SELECT pg_terminate_backend(pid) 
                FROM pg_stat_activity 
                WHERE datname = '{databaseName}' AND pid <> pg_backend_pid();");

            sb.AppendLine($"DROP DATABASE IF EXISTS {databaseName};");

            sb.AppendLine($"CREATE DATABASE {databaseName};");

            return sb.ToString();
        }

        public static string CreateDbSchema()
        {
            var sb = new StringBuilder();

            var scripts = Directory.GetFiles("Core/Scripts/Schema", "*.sql")
                .Select(x => File.ReadAllText(x));

            foreach (string script in scripts)
            {
                sb.AppendLine(script);
            }

            return sb.ToString();
        }

        public static string SeedInitialData()
        {
            return File.ReadAllText("Core/Scripts/InitialData.sql");
        }

        public static string RefreshViews()
        {
            string[] views =
            {
                "get_products_list",
                "get_product_comments",
                "get_product_reviews"
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