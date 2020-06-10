using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories;
using WebFeatures.Common.Extensions;
using WebFeatures.Common.SystemTime;
using WebFeatures.Domian.Common;
using WebFeatures.Infrastructure.DataAccess.Extensions;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Writing
{
    internal class WriteRepository<TEntity> : IWriteRepository<TEntity>
        where TEntity : Entity
    {
        protected IDbConnection Connection { get; }
        protected IEntityProfile Profile { get; }
        protected IEntityMap<TEntity> Entity { get; }

        public WriteRepository(IDbConnection connection, IEntityProfile profile)
        {
            Connection = connection;
            Profile = profile;
            Entity = profile.GetMap<TEntity>();
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            string sql = $"SELECT * FROM {Entity.Table.NameWithSchema()}";

            return Connection.QueryAsync<TEntity>(sql);
        }

        public virtual Task<TEntity> GetAsync(Guid id)
        {
            var sql = string.Format(
                @"SELECT * FROM {0} 
                WHERE {1} = @id",
                Entity.Table.NameWithSchema(),
                Entity.Column(x => x.Id));

            return Connection.QuerySingleOrDefaultAsync<TEntity>(sql, new { id });
        }

        public virtual Task CreateAsync(TEntity entity)
        {
            if (entity.Id == default)
            {
                entity.Id = Guid.NewGuid();
            }

            if (entity is IHasCreateDate cd)
            {
                cd.CreateDate = DateTimeProvider.DateTime.Now;
            }

            var insertColumns = new List<string>();
            var insertParams = new List<string>();
            var param = new DynamicParameters();

            foreach (PropertyMap<TEntity> map in Entity.Properties)
            {
                insertColumns.Add(map.ColumnName);

                string paramName = "@" + map.ColumnName;

                insertParams.Add(paramName);

                param.Add(paramName, map.GetValue(entity));
            }

            string sql = string.Format(
                @"INSERT INTO {0} ({1}) VALUES ({2})",
                Entity.Table.NameWithSchema(),
                insertColumns.JoinString(),
                insertParams.JoinString());

            return Connection.ExecuteAsync(sql, param);
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            var setParams = new List<string>();
            var param = new DynamicParameters();

            foreach (PropertyMap<TEntity> map in Entity.Properties)
            {
                string paramName = "@" + map.ColumnName;

                setParams.Add($"{map.ColumnName} = {paramName}");

                param.Add(paramName, map.GetValue(entity));
            }

            string sql = string.Format(
                @"UPDATE {0} 
                SET {1} 
                WHERE {2} = @{2}",
                Entity.Table.NameWithSchema(),
                setParams.JoinString(),
                Entity.Column(x => x.Id));

            return Connection.ExecuteAsync(sql, param);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            return DeleteAsync(entity.Id);
        }

        public virtual Task DeleteAsync(Guid id)
        {
            string sql = string.Format(
                @"DELETE FROM {0} 
                WHERE {1} = @id",
                Entity.Table.NameWithSchema(),
                Entity.Column(x => x.Id));

            return Connection.ExecuteAsync(sql, new { id });
        }

        public Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            if (!entities.Any())
            {
                return Task.CompletedTask;
            }

            string sql = string.Format(
                @"DELETE FROM {0} 
                WHERE {1} IN ({2})",
                Entity.Table.NameWithSchema(),
                Entity.Column(x => x.Id),
                entities.Select(x => $"'{x.Id}'").JoinString());

            return Connection.ExecuteAsync(sql);
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            string sql = string.Format(
                @"SELECT 1 FROM {0} 
                WHERE {1} = @id",
                Entity.Table.NameWithSchema(),
                Entity.Column(x => x.Id));

            return await Connection.ExecuteScalarAsync<int>(sql, new { id }) == 1;
        }
    }
}