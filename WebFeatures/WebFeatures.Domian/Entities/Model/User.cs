using System.Collections.Generic;
using System.ComponentModel;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Domian.Entities.Model
{
    [Description("Пользователь")]
    public class User : BaseEntity<int>
    {
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public ContactDetails ContactDetails { get; set; }
        public ICollection<Blog> Blogs { get; } = new HashSet<Blog>();
    }
}
