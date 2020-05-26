using System.IO;

namespace WebFeatures.DatabaseInitializer
{
    internal static class SqlBuilder
    {
        public static string CreateDatabase(string databaseName)
            => $"DROP DATABASE IF EXISTS {databaseName};\n" +
               $"CREATE DATABASE {databaseName};\n";

        public static string CreateDbSchema()
            => File.ReadAllText("Scripts/Schema.sql");

        public static string SeedInitialData()
            => File.ReadAllText("Scripts/InitialData.sql");
    }
}
