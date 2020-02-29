using System.Linq;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Posts.GetPosts
{
    public class GetPostsQuery : IQuery<IQueryable<PostInfoDto>>
    {
    }
}
