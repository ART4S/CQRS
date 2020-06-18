using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data.Common;
using System.Threading.Tasks;
using WebFeatures.DbCreator.Core.DataAccess;

namespace WebFeatures.DbCreator.Core
{
    internal class ScriptsExecutor
    {
        private readonly ILogger<ScriptsExecutor> _logger;
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly DbCreateOptions _createOptions;

        public ScriptsExecutor(
            ILogger<ScriptsExecutor> logger,
            IDbConnectionFactory connectionFactory,
            IOptions<DbCreateOptions> createOptions)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
            _createOptions = createOptions.Value;
        }

        public async Task ExecuteAsync()
        {
            if (_createOptions.Development)
            {
                await CreateDatabaseAsync("dev");
            }

            if (_createOptions.Testing)
            {
                await CreateDatabaseAsync("test");
            }
        }

        private async Task CreateDatabaseAsync(string environment)
        {
            await using DbConnection connection = _connectionFactory.CreateConnection();

            await connection.OpenAsync();

            string defaultDb = connection.Database;

            string db = $"webfeatures_{environment}_db";

            _logger.LogInformation($"Creating {environment} database");

            await ExecuteScriptAsync(connection, SqlBuilder.DropDatabase(db));

            await ExecuteScriptAsync(connection, SqlBuilder.CreateDatabase(db));

            await connection.ChangeDatabaseAsync(db);

            await using DbTransaction transaction = await connection.BeginTransactionAsync();

            try
            {
                _logger.LogInformation("Creating schema");

                var schemaScripts = SqlBuilder.GetDbSchemaScripts();

                foreach ((string Name, string Body) script in schemaScripts)
                {
                    _logger.LogInformation($"Executing '{script.Name}'");

                    await ExecuteScriptAsync(connection, script.Body);
                }

                _logger.LogInformation("Seeding initial data");

                await ExecuteScriptAsync(connection, SqlBuilder.SeedInitialData(environment));

                _logger.LogInformation("Updating materialized views");

                await ExecuteScriptAsync(connection, SqlBuilder.RefreshViews());

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();

                await connection.ChangeDatabaseAsync(defaultDb);

                await ExecuteScriptAsync(connection, SqlBuilder.DropDatabase(db));

                throw;
            }
        }

        private async Task ExecuteScriptAsync(DbConnection connection, string script)
        {
            await using DbCommand command = connection.CreateCommand();

            command.CommandText = script;

            await command.ExecuteNonQueryAsync();
        }
    }
}