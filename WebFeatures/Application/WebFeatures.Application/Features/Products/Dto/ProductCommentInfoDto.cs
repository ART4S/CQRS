using System;

namespace WebFeatures.Application.Features.Products.Dto
{
    public class ProductCommentInfoDto
    {
        public Guid Id { get; set; }
        public string Body { get; }
        public DateTime CreateDate { get; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; }
        public Guid? ParentCommentId { get; }
    }
}