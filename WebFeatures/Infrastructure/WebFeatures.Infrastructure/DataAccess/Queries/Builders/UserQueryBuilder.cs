using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Mappings.Utils;
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
            IEntityMap<User> user = Profile.GetMap<User>();
            IEntityMap<UserRole> userRole = Profile.GetMap<UserRole>();
            IEntityMap<Role> role = Profile.GetMap<Role>();

            string query = string.Format(
                @"SELECT * FROM {0} users 
                LEFT JOIN {1} userRoles ON userRoles.{2} = users.{3} 
                LEFT JOIN {4} roles ON roles.{5} = userRoles.{6} 
                WHERE users.{7} = @email",
                user.Table.NameWithSchema(),
                userRole.Table.NameWithSchema(),
                userRole.Column(x => x.UserId),
                user.Column(x => x.Id),
                role.Table.NameWithSchema(),
                role.Column(x => x.Id),
                userRole.Column(x => x.RoleId),
                user.Column(x => x.Email));

            string splitOn = string.Join(", ",
                user.Column(x => x.Id),
                userRole.Column(x => x.UserId),
                role.Column(x => x.Id));

            return new SqlQuery(query, new { email }, splitOn);
        }
    }
}