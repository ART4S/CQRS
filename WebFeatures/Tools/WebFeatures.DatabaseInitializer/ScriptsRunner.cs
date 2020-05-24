using Microsoft.Extensions.Logging;
using System.Data;
using WebFeatures.AppInitializer.Database;
using WebFeatures.AppInitializer.Extensions;

namespace WebFeatures.AppInitializer
{
    internal class ScriptsRunner
    {
        private readonly ILogger<ScriptsRunner> _logger;
        private readonly IDbConnectionFactory _connectionFactory;

        public ScriptsRunner(ILogger<ScriptsRunner> logger, IDbConnectionFactory connectionFactory)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
        }

        public void Run()
        {
            using IDbConnection connection = _connectionFactory.CreateConnection();

            connection.Open();

            string database = "webfeatures_db";

            _logger.LogInformation("Creating database...");
            connection.Execute(SqlBuilder.CreateDatabase(database));

            connection.ChangeDatabase(database);

            _logger.LogInformation("Creating schema...");
            connection.Execute(SqlBuilder.CreateDbSchema());

            _logger.LogInformation("Seeding initial data...");
            connection.Execute(SqlBuilder.SeedInitialData());
        }
    }
}
