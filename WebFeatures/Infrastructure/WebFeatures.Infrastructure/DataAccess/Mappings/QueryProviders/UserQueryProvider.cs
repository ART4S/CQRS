using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Mappings.Queries;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.QueryProviders
{
    internal class UserQueryProvider : QueryProvider<User, UserQueries>
    {
        public UserQueryProvider(IEntityProfile profile) : base(profile)
        {
        }

        protected override UserQueries BuildQueries(EntityMap<User> entity)
        {
            UserQueries queries = base.BuildQueries(entity);
            queries.GetUserByEmail = BuildGetUserByEmail(entity);

            return queries;
        }

        private string BuildGetUserByEmail(EntityMap<User> entity)
        {
            PropertyMap email = entity.GetPropertyMap(x => x.Email);

            string sql =
                $"SELECT * FROM {entity.Table.Schema}.{entity.Table.Name}\n" +
                $"WHERE {email.Field} = @{email.Property}";

            return sql;
        }
    }
}
