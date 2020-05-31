using System;

namespace WebFeatures.Application.Features.Products.Dto
{
    public class ProductCommentInfoDto
    {
        public Guid Id { get; set; }
        public Guid? ParentCommentId { get; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; }
        public DateTime CreatedAt { get; }
        public string Body { get; }
    }
}
