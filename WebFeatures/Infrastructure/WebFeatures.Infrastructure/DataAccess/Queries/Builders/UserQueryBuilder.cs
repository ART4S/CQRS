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
            EntityMap<UserRole> userRole = Profile.GetMappingFor<UserRole>();
            EntityMap<Role> role = Profile.GetMappingFor<Role>();

            string query =
                $"SELECT * FROM {EntityMap.Table.NameWithSchema()} u\n" +
                $"LEFT JOIN {userRole.Table.NameWithSchema()} ur ON ur.{userRole.Field(x => x.UserId)} = u.{EntityMap.Identity.Field}\n" +
                $"LEFT JOIN {role.Table.NameWithSchema()} r ON r.{role.Field(x => x.Id)} = ur.{userRole.Field(x => x.RoleId)}\n" +
                $"WHERE u.{EntityMap.Field(x => x.Email)} = @{nameof(email)}";

            string splitOn = $"{EntityMap.Field(x => x.Id)}, {userRole.Field(x => x.UserId)}, {role.Field(x => x.Id)}";

            return new SqlQuery(query, new { email }, splitOn);
        }
    }
}