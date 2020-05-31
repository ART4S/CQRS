using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Mappings.Utils;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Queries.Builders
{
    internal interface IUserQueryBuilder : IQueryBuilder<User>
    {
        SqlQuery BuildGetUserByEmail(string email);
    }

    internal class UserQueryBuilder : QueryBuilder<User>, IUserQueryBuilder
    {
        public UserQueryBuilder(IEntityProfile profile) : base(profile)
        {
        }

        public SqlQuery BuildGetUserByEmail(string email)
        {
            IEntityMap<User> user = Profile.GetMap<User>();

            string query = string.Format(
                @"SELECT * FROM {0}
                WHERE {1} = @email",
                user.Table.NameWithSchema(),
                user.Column(x => x.Email));

            return new SqlQuery(query, new { email });
        }
    }
}