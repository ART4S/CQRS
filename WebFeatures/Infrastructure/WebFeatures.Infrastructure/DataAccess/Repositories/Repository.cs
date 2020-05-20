using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Repositories;
using WebFeatures.Domian.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Queries;
using WebFeatures.Infrastructure.DataAccess.Mappings.QueryProviders;

namespace WebFeatures.Infrastructure.DataAccess.Repositories
{
    internal class Repository<TEntity, TQueries> : IAsyncRepository<TEntity>
        where TEntity : BaseEntity, new()
        where TQueries : IQueries<TEntity>, new()
    {
        protected readonly IDbConnection Connection;
        protected readonly TQueries Queries;

        public Repository(IDbConnection connection, IQueryProvider<TEntity, TQueries> queriesProvider)
        {
            Connection = connection;
            Queries = queriesProvider.GetQueries();
        }

        public virtual Task CreateAsync(TEntity entity)
        {
            if (entity.Id == default)
            {
                entity.Id = Guid.NewGuid();
            }

            return Connection.ExecuteAsync(Queries.Create, entity);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            return Connection.ExecuteAsync(Queries.Delete, entity);
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            int result = await Connection.ExecuteScalarAsync<int>(Queries.Exists, new { id });

            return result == 1;
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Connection.QueryAsync<TEntity>(Queries.GetAll);
        }

        public virtual Task<TEntity> GetAsync(Guid id)
        {
            return Connection.QuerySingleOrDefaultAsync<TEntity>(Queries.Get, new TEntity { Id = id });
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            return Connection.ExecuteAsync(Queries.Update, entity);
        }
    }
}
