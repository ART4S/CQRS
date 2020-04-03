using System;
using System.Collections.Generic;
using WebFeatures.Domian.Attibutes;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    [EntityMetadata("Users")]
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public Guid? PictureId { get; set; }
        public File Picture { get; set; }

        public ICollection<UserRole> UserRoles { get; private set; } = new HashSet<UserRole>();
    }
}
