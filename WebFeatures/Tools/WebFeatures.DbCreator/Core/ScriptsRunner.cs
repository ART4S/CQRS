using Microsoft.Extensions.Logging;
using System.Data;
using WebFeatures.DbCreator.Core.DataAccess;
using WebFeatures.DbCreator.Core.Extensions;

namespace WebFeatures.DbCreator.Core
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

            string defaultDbName = connection.Database;

            const string appDbName = "webfeatures_db";

            _logger.LogInformation("Creating database");

            connection.Execute(SqlBuilder.DropDatabase(appDbName));

            connection.Execute(SqlBuilder.CreateDatabase(appDbName));

            connection.ChangeDatabase(appDbName);

            using IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                _logger.LogInformation("Creating schema");

                var schemaScripts = SqlBuilder.GetDbSchemaScripts();

                foreach ((string Name, string Body) script in schemaScripts)
                {
                    _logger.LogInformation($"Executing '{script.Name}'");

                    connection.Execute(script.Body);
                }

                _logger.LogInformation("Seeding initial data");

                connection.Execute(SqlBuilder.SeedInitialData());

                _logger.LogInformation("Updating materialized views");

                connection.Execute(SqlBuilder.RefreshViews());

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();

                connection.ChangeDatabase(defaultDbName);

                connection.Execute(SqlBuilder.DropDatabase(appDbName));

                throw;
            }
        }
    }
}