using System.Linq;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Blogs.GetBlogs
{
    public class GetBlogsQuery : IQuery<IQueryable<BlogInfoDto>>
    {
    }
}
