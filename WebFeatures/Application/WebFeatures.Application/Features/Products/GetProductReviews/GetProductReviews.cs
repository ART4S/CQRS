using System;
using System.Collections.Generic;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Products.GetProductReviews
{
    public class GetProductReviews : IQuery<IEnumerable<ProductReviewInfoDto>>
    {
        public Guid ProductId { get; set; }
    }
}
