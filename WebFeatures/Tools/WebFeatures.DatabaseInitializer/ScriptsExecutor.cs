using Microsoft.Extensions.Logging;
using System.Data;
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

            string database = "webfeatures_db";

            _logger.LogInformation("Creating database...");

            connection.ExecuteScript(
                ScriptsProvider.CreateDbScript(database));

            connection.ChangeDatabase(database);

            _logger.LogInformation("Initializing schema...");

            connection.ExecuteScript(
                ScriptsProvider.InitDbSchemaScript());

            _logger.LogInformation("Seeding initial data...");

            connection.ExecuteScript(
                ScriptsProvider.InitialDataScript());
        }
    }
}
