using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;

namespace WebFeatures.Application.Features.Posts.DeletePost
{
    public class DeletePostCommand : ICommand<Unit>
    {
        public int Id { get; set; }
    }
}
