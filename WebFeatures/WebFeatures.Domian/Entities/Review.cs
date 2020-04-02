using System;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Domian.Entities
{
    public class Review : BaseEntity
    {
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserRating Rating { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
