using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WebFeatures.DbCreator.Core
{
    internal static class SqlBuilder
    {
        public static string CreateDatabase(string databaseName)
        {
            return $"CREATE DATABASE {databaseName};";
        }

        public static string DropDatabase(string databaseName)
        {
            var sb = new StringBuilder();

            sb.AppendLine(
                @$"SELECT pg_terminate_backend(pid) 
                FROM pg_stat_activity 
                WHERE datname = '{databaseName}' AND pid <> pg_backend_pid();");

            sb.AppendLine($"DROP DATABASE IF EXISTS {databaseName};");

            return sb.ToString();
        }

        public static IEnumerable<(string Name, string Body)> GetDbSchemaScripts()
        {
            var files = Directory.GetFiles("Core/Scripts/Schema", "*.sql");

            foreach (string file in files)
            {
                yield return (file, File.ReadAllText(file));
            }
        }

        public static string SeedInitialData()
        {
            return File.ReadAllText("Core/Scripts/InitialData.sql");
        }

        public static string RefreshViews()
        {
            string[] views =
            {
                "get_products_list"
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