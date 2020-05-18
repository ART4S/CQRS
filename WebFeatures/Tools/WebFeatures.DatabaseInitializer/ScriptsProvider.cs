using System.IO;

namespace WebFeatures.AppInitializer
{
    internal static class ScriptsProvider
    {
        public static string CreateDbScript(string databaseName)
            => $"DROP DATABASE IF EXISTS {databaseName};\n" +
               $"CREATE DATABASE {databaseName};\n";

        public static string InitDbSchemaScript()
            => File.ReadAllText("Scripts/Schema.sql");

        public static string InitialDataScript()
            => File.ReadAllText("Scripts/Data.sql");
    }
}
