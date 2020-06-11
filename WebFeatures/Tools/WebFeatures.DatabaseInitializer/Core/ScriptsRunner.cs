using Microsoft.Extensions.Logging;
using System.Data;
using WebFeatures.DatabaseInitializer.Core.Database;
using WebFeatures.DatabaseInitializer.Core.Extensions;

namespace WebFeatures.DatabaseInitializer.Core
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

            string defaultDb = connection.Database;

            const string appDb = "webfeatures_db";

            _logger.LogInformation("Creating database");

            connection.Execute(SqlBuilder.DropDatabase(appDb));

            connection.Execute(SqlBuilder.CreateDatabase(appDb));

            connection.ChangeDatabase(appDb);

            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                _logger.LogInformation("Creating schema");

                connection.Execute(SqlBuilder.CreateDbSchema());

                _logger.LogInformation("Seeding initial data");

                connection.Execute(SqlBuilder.SeedInitialData());

                _logger.LogInformation("Updating materialized views");

                connection.Execute(SqlBuilder.RefreshViews());

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();

                connection.ChangeDatabase(defaultDb);

                connection.Execute(SqlBuilder.DropDatabase(appDb));

                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}