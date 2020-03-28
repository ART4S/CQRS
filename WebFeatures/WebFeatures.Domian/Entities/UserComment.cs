using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class UserComment : BaseEntity
    {
        public Guid ProductId { get; }
        public Product Product { get; }

        public Guid AuthorId { get; }
        public User Author { get; }

        public DateTime CreatedAt { get; }
        public string Body { get; }

        public Guid? ParentCommentId { get; }
        public UserComment ParentComment { get; }

        public UserComment(Guid productId, Guid authorId, string body, DateTime createdAt, Guid? parentCommentId = null)
        {
            ProductId = productId;
            AuthorId = authorId;
            Body = body;
            CreatedAt = createdAt;
            ParentCommentId = parentCommentId;
        }

        private UserComment() { } // For EF
    }
}
