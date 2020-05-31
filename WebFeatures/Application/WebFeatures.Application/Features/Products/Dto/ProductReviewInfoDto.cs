using System;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Application.Features.Products.Dto
{
    public class ProductReviewInfoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public ProductRating Rating { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public Guid ProductId { get; set; }
    }
}
