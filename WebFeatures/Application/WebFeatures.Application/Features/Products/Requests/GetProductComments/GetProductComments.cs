using System;
using System.Linq;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Products.Requests.GetProductComments
{
    public class GetProductComments : IQuery<IQueryable<CommentInfoDto>>
    {
        public Guid ProductId { get; set; }
    }
}
