using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities.Permissions
{
    public class RolePermission : Entity
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public string Name { get; set; }
    }
}
