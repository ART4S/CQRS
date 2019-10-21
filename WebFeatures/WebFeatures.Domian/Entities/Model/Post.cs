using System;
using System.ComponentModel;
using WebFeatures.Domian.Entities.Abstractions;
using WebFeatures.Specifications;

namespace WebFeatures.Domian.Entities.Model
{
    [Description("Пост")]
    public class Post : BaseEntity, IUpdatable
    {
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static class Specs
        {
            public static readonly Spec<Post> IsAuthorNotAdmin = new Spec<Post>(p => p.Blog.Author.Name != "Admin"); // example spec
        }
    }
}
