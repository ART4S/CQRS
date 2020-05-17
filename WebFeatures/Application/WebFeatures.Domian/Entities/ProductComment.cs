using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class ProductComment : BaseEntity, IHasCreateDate
    {
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public Guid? ParentCommentId { get; set; }
        public ProductComment ParentComment { get; set; }
    }
}
