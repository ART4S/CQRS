using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities.Accounts
{
    public class RolePermission : Entity
    {
        public string Name { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
