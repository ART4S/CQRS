using System;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Domian.Entities.Permissions
{
    public class RolePermission : Entity
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public Permission Permission { get; set; }
    }
}
