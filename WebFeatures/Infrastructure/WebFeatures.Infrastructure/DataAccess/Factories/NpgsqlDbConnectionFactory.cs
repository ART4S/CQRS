using Npgsql;
using System;
using System.Data;
using WebFeatures.Common;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure.DataAccess.Factories
{
    internal class NpgsqlDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public NpgsqlDbConnectionFactory(string connectionString)
        {
            Guard.ThrowIfNull(connectionString, nameof(connectionString));

            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
