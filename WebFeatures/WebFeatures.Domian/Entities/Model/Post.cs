using System;
using System.ComponentModel;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Domian.Entities.Model
{
    [Description("Пост")]
    public class Post : BaseEntity<int>, IUpdatable
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
