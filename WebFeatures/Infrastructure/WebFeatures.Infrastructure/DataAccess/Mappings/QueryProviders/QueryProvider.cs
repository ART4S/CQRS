using System.Data;
using System.Linq;
using WebFeatures.Domian.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Mappings.Queries;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.QueryProviders
{
    internal class QueryProvider<TEntity, TQueries> : IQueryProvider<TEntity, TQueries>
        where TEntity : BaseEntity
        where TQueries : IQueries<TEntity>, new()
    {
        private readonly EntityMap<TEntity> _entity;
        private TQueries _queries;

        public QueryProvider(IEntityProfile profile)
        {
            _entity = profile.GetMappingFor<TEntity>();
        }

        public TQueries GetQueries() => _queries ?? (_queries = BuildQueries(_entity));

        protected virtual TQueries BuildQueries(EntityMap<TEntity> entity)
        {
            return new TQueries()
            {
                GetAll = BuildGetAll(entity),
                Get = BuildGet(entity),
                Create = BuildCreate(entity),
                Update = BuildUpdate(entity),
                Delete = BuildDelete(entity),
                Exists = BuildExists(entity)
            };
        }

        private string BuildGetAll(EntityMap<TEntity> entity)
        {
            string sql = $"SELECT * FROM {entity.Table.Schema}.{entity.Table.Name}";

            return sql;
        }

        private string BuildGet(EntityMap<TEntity> entity)
        {
            string sql =
                $"SELECT * FROM {entity.Table.Schema}.{entity.Table.Name}\n" +
                $"WHERE {entity.Identity.Field} = @{entity.Identity.Property}";

            return sql;
        }

        private string BuildCreate(EntityMap<TEntity> entity)
        {
            string sql =
                $"INSERT INTO {entity.Table.Schema}.{entity.Table.Name} ({BuildInsertFields(entity)})\n" +
                $"VALUES ({BuildInsertParams(entity)})";

            string BuildInsertFields(EntityMap<TEntity> entity)
            {
                return string.Join(", ", entity.Mappings.Select(x => x.Field));
            }

            string BuildInsertParams(EntityMap<TEntity> entity)
            {
                return string.Join(", ", entity.Mappings.Select(x => "@" + x.Property));
            }

            return sql;
        }

        private string BuildUpdate(EntityMap<TEntity> entity)
        {
            string sql =
                $"UPDATE {entity.Table.Schema}.{entity.Table.Name}\n" +
                $"SET {BuildSetParams(entity)}\n" +
                $"WHERE {entity.Identity.Field} = @{entity.Identity.Property}";

            string BuildSetParams(EntityMap<TEntity> entity)
            {
                return string.Join(", ", entity.Mappings.Select(x => $"{x.Field} = @{x.Property}"));
            }

            return sql;
        }

        private string BuildDelete(EntityMap<TEntity> entity)
        {
            string sql =
                $"DELETE FROM {entity.Table.Schema}.{entity.Table.Name}\n" +
                $"WHERE {entity.Identity.Field} = @{entity.Identity.Property}";

            return sql;
        }

        private string BuildExists(EntityMap<TEntity> entity)
        {
            string sql =
                $"SELECT 1 FROM {entity.Table.Schema}.{entity.Table.Name}\n" +
                $"WHERE {entity.Identity.Field} = @{entity.Identity.Property}";

            return sql;
        }
    }
}
