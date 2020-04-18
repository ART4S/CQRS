using System;
using WebFeatures.Domian.Attibutes;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    [EntityMetadata("UserComments")]
    public class UserComment : BaseEntity, IHasCreateDate
    {
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public Guid? ParentCommentId { get; set; }
        public UserComment ParentComment { get; set; }
    }
}
