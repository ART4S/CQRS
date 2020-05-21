using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Helpers;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Queries.Builders
{
    internal class UserQueryBuilder : QueryBuilder<User>
    {
        public UserQueryBuilder(IEntityProfile profile) : base(profile)
        {
        }

        public SqlQuery BuildGetUserByEmail(string email)
        {
            EntityMap<UserRole> userRoleMap = Profile.GetMappingFor<UserRole>();

            string query =
                $"SELECT * FROM {EntityMap.Table.NameWithSchema()} u\n" +
                $"WHERE {EntityMap.Field(x => x.Email)} = @{nameof(email)}" +
                $"LEFT JOIN {userRoleMap.Table.NameWithSchema()} ur ON ur.{userRoleMap.Field(x => x.UserId)} = u.{EntityMap.Identity.Field}";

            return new SqlQuery(query, new { email });
        }
    }
}