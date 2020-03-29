using System;
using WebFeatures.Common;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Domian.Entities
{
    public class Review : BaseEntity
    {
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string Title { get; private set; }
        public string Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public UserRating Rating { get; private set; }

        public Review(Guid authorId, Guid productId, string title, string comment, DateTime createdAt, UserRating rating)
        {
            Guard.NotNullOrWhitespace(title, nameof(title));
            Guard.NotNullOrWhitespace(comment, nameof(comment));

            AuthorId = authorId;
            ProductId = productId;
            Title = title;
            Comment = comment;
            CreatedAt = createdAt;
            Rating = rating;
        }

        private Review() { } // For EF
    }
}
