using WebFeatures.Domian.Entities;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Queries
{
    internal class UserQueries : Queries<User>
    {
        public string GetUserByEmail { get; set; }
    }
}
