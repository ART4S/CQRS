using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Posts.GetPostById
{
    public class GetPostByIdQuery : IQuery<PostEditDto>
    {
        public int Id { get; set; }
    }
}
