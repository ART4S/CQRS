using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings
{
    internal class UserRolesMap : EntityMap<UserRole>
    {
        public UserRolesMap()
        {
            WithoutIdentity();
        }
    }
}
