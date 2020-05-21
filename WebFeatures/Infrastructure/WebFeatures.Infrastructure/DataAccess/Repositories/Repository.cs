using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Repositories;
using WebFeatures.Domian.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Querying;

namespace WebFeatures.Infrastructure.DataAccess.Repositories
{
    internal class Repository<TEntity, TQueries> : IAsyncRepository<TEntity>
        where TEntity : Entity, new()
        where TQueries : Queries, new()
    {
        protected readonly IDbConnection Connection;
        protected readonly TQueries Queries;

        public Repository(IDbConnection connection, TQueries queries)
        {
            Connection = connection;
            Queries = queries;
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Connection.QueryAsync<TEntity>(Queries.GetAll);
        }

        public virtual Task<TEntity> GetAsync(Guid id)
        {
            return Connection.QuerySingleOrDefaultAsync<TEntity>(Queries.Get, new TEntity { Id = id });
        }

        public virtual Task CreateAsync(TEntity entity)
        {
            if (entity.Id == default)
            {
                entity.Id = Guid.NewGuid();
            }

            return Connection.ExecuteAsync(Queries.Create, entity);
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            return Connection.ExecuteAsync(Queries.Update, entity);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            return Connection.ExecuteAsync(Queries.Delete, entity);
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            return await Connection.ExecuteScalarAsync<int>(Queries.Exists, new TEntity { Id = id }) == 1;
        }
    }
}
