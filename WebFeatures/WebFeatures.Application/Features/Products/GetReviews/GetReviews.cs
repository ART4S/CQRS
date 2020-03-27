using System;
using System.Linq;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Products.GetReviews
{
    public class GetReviews : IQuery<IQueryable<ReviewInfoDto>>
    {
        public Guid ProductId { get; set; }
    }
}
