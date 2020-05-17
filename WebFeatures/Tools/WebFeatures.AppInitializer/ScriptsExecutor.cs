using Microsoft.Extensions.Logging;
using System.Data;
using System.IO;
using WebFeatures.AppInitializer.Database;
using WebFeatures.AppInitializer.Extensions;

namespace WebFeatures.AppInitializer
{
    internal class ScriptsExecutor
    {
        private readonly ILogger<ScriptsExecutor> _logger;
        private readonly IDbConnectionFactory _connectionFactory;

        public ScriptsExecutor(ILogger<ScriptsExecutor> logger, IDbConnectionFactory connectionFactory)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
        }

        public void Execute()
        {
            using IDbConnection connection = _connectionFactory.CreateConnection();

            connection.Open();

            string database = "webfeatures";

            CreateDb(connection, database);

            connection.ChangeDatabase(database);

            InitDbSchema(connection);
        }

        private void CreateDb(IDbConnection connection, string databaseName)
        {
            _logger.LogInformation("Executing CreateDb...");

            string script = $"DROP DATABASE IF EXISTS {databaseName};\n" +
                            $"CREATE DATABASE {databaseName};\n";

            connection.ExecuteScript(script);
        }

        private void InitDbSchema(IDbConnection connection)
        {
            _logger.LogInformation("Executing InitDbSchema...");

            string script = File.ReadAllText("Scripts/Schema.sql");
            connection.ExecuteScript(script);
        }
    }
}
