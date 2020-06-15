using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using WebFeatures.DbCreator.Core.DataAccess;
using WebFeatures.DbCreator.Core.Extensions;

namespace WebFeatures.DbCreator.Core
{
    internal class ScriptsRunner
    {
        private readonly ILogger<ScriptsRunner> _logger;
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly DbCreateOptions _createOptions;

        public ScriptsRunner(
            ILogger<ScriptsRunner> logger,
            IDbConnectionFactory connectionFactory,
            IOptions<DbCreateOptions> createOptions)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
            _createOptions = createOptions.Value;
        }

        public void Run()
        {
            if (_createOptions.Development)
            {
                CreateDatabase("dev");
            }

            if (_createOptions.Testing)
            {
                CreateDatabase("test");
            }
        }

        private void CreateDatabase(string environment)
        {
            using IDbConnection connection = _connectionFactory.CreateConnection();

            connection.Open();

            string defaultDb = connection.Database;

            string db = $"webfeatures_{environment}_db";

            _logger.LogInformation($"Creating {environment} database");

            connection.Execute(SqlBuilder.DropDatabase(db));

            connection.Execute(SqlBuilder.CreateDatabase(db));

            connection.ChangeDatabase(db);

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

                connection.Execute(SqlBuilder.SeedInitialData(environment));

                _logger.LogInformation("Updating materialized views");

                connection.Execute(SqlBuilder.RefreshViews());

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();

                connection.ChangeDatabase(defaultDb);

                connection.Execute(SqlBuilder.DropDatabase(db));

                throw;
            }
        }
    }
}