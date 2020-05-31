using System;
using System.Collections.Generic;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Products.Requests.Queries
{
    public class GetProductComments : IQuery<IEnumerable<ProductCommentInfoDto>>
    {
        public Guid ProductId { get; set; }
    }
}
