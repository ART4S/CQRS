using Dapper;
using System;
using System.Collections.Generic;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Mappings.Utils;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Queries.Builders
{
    internal interface IQueryBuilder<TEntity> where TEntity : class
    {
        SqlQuery BuildGetAll();
        SqlQuery BuildGet(Guid id);
        SqlQuery BuildCreate(TEntity entity);
        SqlQuery BuildUpdate(TEntity entity);
        SqlQuery BuildDelete(TEntity entity);
        SqlQuery BuildExists(Guid id);
    }

    internal class QueryBuilder<TEntity> : IQueryBuilder<TEntity>
        where TEntity : class
    {
        protected IEntityProfile Profile { get; }

        public QueryBuilder(IEntityProfile profile)
        {
            Profile = profile;
        }

        public virtual SqlQuery BuildGetAll()
        {
            IEntityMap<TEntity> entityMap = Profile.GetMap<TEntity>();

            string query = $"SELECT * FROM {entityMap.Table.NameWithSchema()}";

            return new SqlQuery(query);
        }

        public virtual SqlQuery BuildGet(Guid id)
        {
            IEntityMap<TEntity> entityMap = Profile.GetMap<TEntity>();

            var query = string.Format(
                @"SELECT * FROM {0} 
                WHERE {1} = @id",
                entityMap.Table.NameWithSchema(),
                entityMap.Identity.Column);

            return new SqlQuery(query, new { id });
        }

        public virtual SqlQuery BuildCreate(TEntity entity)
        {
            IEntityMap<TEntity> entityMap = Profile.GetMap<TEntity>();

            var insertColumns = new List<string>();
            var insertParams = new List<string>();
            var param = new DynamicParameters();

            foreach (PropertyMap<TEntity> map in entityMap.Properties)
            {
                insertColumns.Add(map.Column);

                string paramName = "@" + map.Column;

                insertParams.Add(paramName);

                param.Add(paramName, map.GetValue(entity));
            }

            string query = string.Format(
                @"INSERT INTO {0} ({1}) VALUES ({2})",
                entityMap.Table.NameWithSchema(),
                string.Join(", ", insertColumns),
                string.Join(", ", insertParams));

            return new SqlQuery(query, param);
        }

        public virtual SqlQuery BuildUpdate(TEntity entity)
        {
            IEntityMap<TEntity> entityMap = Profile.GetMap<TEntity>();

            var setParams = new List<string>();
            var param = new DynamicParameters();

            foreach (PropertyMap<TEntity> map in entityMap.Properties)
            {
                string paramName = "@" + map.Column;

                setParams.Add($"{map.Column} = {paramName}");

                param.Add(paramName, map.GetValue(entity));
            }

            string query = string.Format(
                @"UPDATE {0} 
                SET {1} 
                WHERE {2} = @{2}",
                entityMap.Table.NameWithSchema(),
                string.Join(", ", setParams),
                entityMap.Identity.Column);

            return new SqlQuery(query, param);
        }

        public virtual SqlQuery BuildDelete(TEntity entity)
        {
            IEntityMap<TEntity> entityMap = Profile.GetMap<TEntity>();

            string query = string.Format(
                @"DELETE FROM {0} 
                WHERE {1} = @{2}",
                entityMap.Table.NameWithSchema(),
                entityMap.Identity.Column,
                entityMap.Identity.PropertyName);

            return new SqlQuery(query, entity);
        }

        public virtual SqlQuery BuildExists(Guid id)
        {
            IEntityMap<TEntity> entityMap = Profile.GetMap<TEntity>();

            string query = string.Format(
                @"SELECT 1 FROM {0} 
                WHERE {1} = @id",
                entityMap.Table.NameWithSchema(),
                entityMap.Identity.Column);

            return new SqlQuery(query, new { id });
        }
    }
}