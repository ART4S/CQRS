using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public Guid? PictureId { get; set; }
        public File Picture { get; set; }
    }
}
