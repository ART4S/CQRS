using System.Collections.Generic;
using System.ComponentModel;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Domian.Entities.Model
{
    [Description("Пользователь")]
    public class User : BaseEntity<int>
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
        }

        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public virtual ContactDetails ContactDetails { get; set; }

        public virtual ICollection<Blog> Blogs { get; }
    }
}
