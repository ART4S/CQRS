using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SqlMapper = Dapper.SqlMapper;

namespace WebFeatures.Infrastructure.DataAccess.QueryExecutors
{
    internal interface IDbExecutor
    {
        Task ExecuteAsync(IDbConnection connection, string sql, object param);
        Task ExecuteAsync(IDbConnection connection, string sql);
        Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string sql, object param);
        Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string sql, object param, CommandType commandType);
        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object param);
        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql);
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(IDbConnection connection, string sql, Func<TFirst, TSecond, TReturn> map, string splitOn);
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(IDbConnection connection, string sql, object param, Func<TFirst, TSecond, TReturn> map, string splitOn);
        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, CommandType commandType);
        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object param, CommandType commandType);
        Task<T> QuerySingleOrDefaultAsync<T>(IDbConnection connection, string sql, object param);
        Task<T> QuerySingleOrDefaultAsync<T>(IDbConnection connection, string sql, object param, CommandType commandType);
    }

    internal class DapperDbExecutor : IDbExecutor
    {
        public Task ExecuteAsync(IDbConnection connection, string sql, object param = null)
            => SqlMapper.ExecuteAsync(connection, sql, param);

        public Task ExecuteAsync(IDbConnection connection, string sql)
            => SqlMapper.ExecuteAsync(connection, sql);

        public Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string sql, object param)
            => SqlMapper.ExecuteScalarAsync<T>(connection, sql, param);

        public Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string sql, object param, CommandType commandType)
            => SqlMapper.ExecuteScalarAsync<T>(connection, sql, param, commandType: commandType);

        public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object param)
            => SqlMapper.QueryAsync<T>(connection, sql, param);

        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(IDbConnection connection, string sql, Func<TFirst, TSecond, TReturn> map, string splitOn)
            => SqlMapper.QueryAsync(connection, sql, map, splitOn: splitOn);

        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(IDbConnection connection, string sql, object param, Func<TFirst, TSecond, TReturn> map, string splitOn)
            => SqlMapper.QueryAsync(connection, sql, map, param, splitOn: splitOn);

        public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, CommandType commandType)
            => SqlMapper.QueryAsync<T>(connection, sql, commandType: commandType);

        public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object param, CommandType commandType)
            => SqlMapper.QueryAsync<T>(connection, sql, param, commandType: commandType);

        public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql)
            => SqlMapper.QueryAsync<T>(connection, sql);

        public Task<T> QuerySingleOrDefaultAsync<T>(IDbConnection connection, string sql, object param = null)
            => SqlMapper.QuerySingleOrDefaultAsync<T>(connection, sql, param);

        public Task<T> QuerySingleOrDefaultAsync<T>(IDbConnection connection, string sql, object param, CommandType commandType)
            => SqlMapper.QuerySingleOrDefaultAsync<T>(connection, sql, param, commandType: commandType);
    }
}