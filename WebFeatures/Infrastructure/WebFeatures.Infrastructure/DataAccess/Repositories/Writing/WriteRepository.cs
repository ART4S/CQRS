using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories;
using WebFeatures.Domian.Common;
using WebFeatures.Infrastructure.DataAccess.Queries.Builders;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Writing
{
    internal class WriteRepository<TEntity, TQueryBuilder> : IWriteRepository<TEntity>
        where TEntity : IdentityEntity
        where TQueryBuilder : IQueryBuilder<TEntity>
    {
        protected IDbConnection Connection { get; }
        protected TQueryBuilder QueryBuilder { get; }

        public WriteRepository(IDbConnection connection, TQueryBuilder queryBuilder)
        {
            Connection = connection;
            QueryBuilder = queryBuilder;
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            SqlQuery sql = QueryBuilder.BuildGetAll();

            return Connection.QueryAsync<TEntity>(sql.Query, sql.Param);
        }

        public virtual Task<TEntity> GetAsync(Guid id)
        {
            SqlQuery sql = QueryBuilder.BuildGet(id);

            return Connection.QuerySingleOrDefaultAsync<TEntity>(sql.Query, sql.Param);
        }

        public virtual Task CreateAsync(TEntity entity)
        {
            if (entity.Id == default)
            {
                entity.Id = Guid.NewGuid();
            }

            SqlQuery sql = QueryBuilder.BuildCreate(entity);

            return Connection.ExecuteAsync(sql.Query, sql.Param);
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            SqlQuery sql = QueryBuilder.BuildUpdate(entity);

            return Connection.ExecuteAsync(sql.Query, sql.Param);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            SqlQuery sql = QueryBuilder.BuildDelete(entity);

            return Connection.ExecuteAsync(sql.Query, sql.Param);
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            SqlQuery sql = QueryBuilder.BuildExists(id);

            return await Connection.ExecuteScalarAsync<int>(sql.Query, sql.Param) == 1;
        }
    }
}