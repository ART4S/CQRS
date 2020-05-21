using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Queries.Builders
{
    internal class UserQueryBuilder : QueryBuilder<User>
    {
        public UserQueryBuilder(EntityMap<User> entityMap) : base(entityMap)
        {
        }

        public SqlQuery BuildGetUserByEmail(string email)
        {
            PropertyMap emailMap = EntityMap.GetPropertyMap(x => x.Email);

            string query =
                $"SELECT * FROM {EntityMap.Table.Schema}.{EntityMap.Table.Name}\n" +
                $"WHERE {emailMap.Field} = @{nameof(email)}";

            return new SqlQuery(query, new { email });
        }
    }
}