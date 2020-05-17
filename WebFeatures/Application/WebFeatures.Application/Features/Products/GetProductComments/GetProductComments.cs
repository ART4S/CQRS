using System;
using System.Collections.Generic;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Products.GetProductComments
{
    public class GetProductComments : IQuery<IEnumerable<ProductCommentInfoDto>>
    {
        public Guid ProductId { get; set; }
    }
}
