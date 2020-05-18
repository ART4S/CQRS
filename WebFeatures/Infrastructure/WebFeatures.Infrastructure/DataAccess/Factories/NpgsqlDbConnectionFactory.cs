using Npgsql;
using System;
using System.Data;

namespace WebFeatures.Infrastructure.DataAccess.Factories
{
    internal class NpgsqlDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public NpgsqlDbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
