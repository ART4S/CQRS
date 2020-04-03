using System;
using WebFeatures.Domian.Attibutes;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    [TableName("UserComments")]
    public class UserComment : BaseEntity
    {
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public Guid? ParentCommentId { get; set; }
        public UserComment ParentComment { get; set; }
    }
}
