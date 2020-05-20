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
        private static TQueries _queries;

        public QueryProvider(IEntityProfile profile)
        {
            _queries = _queries ?? BuildQueries(profile.GetMappingFor<TEntity>());
        }

        public TQueries GetQueries() => _queries;

        protected virtual TQueries BuildQueries(IEntityMap entity)
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

        private string BuildGetAll(IEntityMap entity)
        {
            string sql = $"SELECT * FROM {entity.Table}";

            return sql;
        }

        private string BuildGet(IEntityMap entity)
        {
            string sql = $"SELECT * FROM {entity.Table} WHERE {entity.Identity.Field} = @id";

            return sql;
        }

        private string BuildCreate(IEntityMap entity)
        {
            string sql =
                $"INSERT INTO {entity.Table} ({BuildInsertFields(entity)})\n" +
                $"VALUES ({BuildInsertParams(entity)})";

            string BuildInsertFields(IEntityMap entity)
            {
                return string.Join(", ", entity.Mappings.Select(x => x.Field));
            }

            string BuildInsertParams(IEntityMap entity)
            {
                return string.Join(", ", entity.Mappings.Select(x => "@" + x.Property));
            }

            return sql;
        }

        private string BuildUpdate(IEntityMap entity)
        {
            string sql =
                $"UPDATE {entity.Table}\n" +
                $"SET {BuildSetParams(entity)}\n" +
                $"WHERE {entity.Identity.Field} = @{entity.Identity.Property}";

            string BuildSetParams(IEntityMap entity)
            {
                return string.Join(", ", entity.Mappings.Select(x => $"{x.Field} = @{x.Property}"));
            }

            return sql;
        }

        private string BuildDelete(IEntityMap entity)
        {
            string sql =
                $"DELETE FROM {entity.Table}\n" +
                $"WHERE {entity.Identity.Field} = @{entity.Identity.Property}";

            return sql;
        }

        private string BuildExists(IEntityMap entity)
        {
            string sql =
                $"SELECT 1 FROM {entity.Table}\n" +
                $"WHERE {entity.Identity.Field} = @{entity.Identity.Property}";

            return sql;
        }
    }
}
