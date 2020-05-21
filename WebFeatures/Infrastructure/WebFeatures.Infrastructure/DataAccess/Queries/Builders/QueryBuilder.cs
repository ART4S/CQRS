using System;
using System.Data;
using System.Linq;
using WebFeatures.Domian.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Queries.Builders
{
    internal class QueryBuilder<TEntity> where TEntity : Entity
    {
        protected EntityMap<TEntity> EntityMap { get; }

        public QueryBuilder(EntityMap<TEntity> entityMap)
        {
            EntityMap = entityMap;
        }

        public SqlQuery BuildGetAll()
        {
            string query = $"SELECT * FROM {EntityMap.Table.Schema}.{EntityMap.Table.Name}";

            return new SqlQuery(query);
        }

        public SqlQuery BuildGet(Guid id)
        {
            string query =
                $"SELECT * FROM {EntityMap.Table.Schema}.{EntityMap.Table.Name}\n" +
                $"WHERE {EntityMap.Identity.Field} = @{nameof(id)}";

            return new SqlQuery(query, new { id });
        }

        public SqlQuery BuildCreate(TEntity entity)
        {
            string query =
                $"INSERT INTO {EntityMap.Table.Schema}.{EntityMap.Table.Name} ({BuildInsertFields()})\n" +
                $"VALUES ({BuildInsertParams()})";

            string BuildInsertFields() => string.Join(", ", EntityMap.Mappings.Select(x => x.Field));
            string BuildInsertParams() => string.Join(", ", EntityMap.Mappings.Select(x => "@" + x.Property));

            return new SqlQuery(query, entity);
        }

        public SqlQuery BuildUpdate(TEntity entity)
        {
            string query =
                $"UPDATE {EntityMap.Table.Schema}.{EntityMap.Table.Name}\n" +
                $"SET {BuildSetParams()}\n" +
                $"WHERE {EntityMap.Identity.Field} = @{EntityMap.Identity.Property}";

            string BuildSetParams()
                => string.Join(", ", EntityMap.Mappings.Select(x => $"{x.Field} = @{x.Property}"));

            return new SqlQuery(query, entity);
        }

        public SqlQuery BuildDelete(TEntity entity)
        {
            string query =
                $"DELETE FROM {EntityMap.Table.Schema}.{EntityMap.Table.Name}\n" +
                $"WHERE {EntityMap.Identity.Field} = @{EntityMap.Identity.Property}";

            return new SqlQuery(query, entity);
        }

        public SqlQuery BuildExists(Guid id)
        {
            string query =
                $"SELECT 1 FROM {EntityMap.Table.Schema}.{EntityMap.Table.Name}\n" +
                $"WHERE {EntityMap.Identity.Field} = @{nameof(id)}";

            return new SqlQuery(query, new { id });
        }
    }
}