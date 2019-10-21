using System.Collections.Generic;
using System.ComponentModel;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Domian.Entities.Model
{
    [Description("Блог")]
    public class Blog : BaseEntity
    {
        public Blog()
        {
            Posts = new HashSet<Post>();
        }

        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Post> Posts { get; }
    }
}
