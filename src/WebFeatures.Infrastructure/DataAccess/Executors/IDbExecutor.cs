using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace WebFeatures.Infrastructure.DataAccess.Executors
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
}