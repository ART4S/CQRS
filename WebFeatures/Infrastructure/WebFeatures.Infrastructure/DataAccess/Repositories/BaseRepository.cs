using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Repositories;
using WebFeatures.Domian.Common;

namespace WebFeatures.Infrastructure.DataAccess.Repositories
{
    public abstract class BaseRepository<TEntity> : IAsyncRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected IDbConnection Connection;

        protected BaseRepository(IDbConnection connection)
        {
            Connection = connection;
        }
        public abstract Task<IEnumerable<TEntity>> GetAllAsync();
        public abstract Task<TEntity> GetAsync(Guid id);
        public abstract Task CreateAsync(TEntity entity);
        public abstract Task UpdateAsync(TEntity entity);
        public abstract Task DeleteAsync(TEntity entity);
        public abstract Task<bool> ExistsAsync(Guid id);
    }
}
