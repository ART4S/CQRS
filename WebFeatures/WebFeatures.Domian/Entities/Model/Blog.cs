using System.Collections.Generic;
using System.ComponentModel;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Domian.Entities.Model
{
    [Description("Блог")]
    public class Blog : BaseEntity<int>
    {
        public int AuthorId { get; set; }
        public User Author { get; set; }

        public string Title { get; set; }

        public ICollection<Post> Posts { get; } = new HashSet<Post>();
    }
}
