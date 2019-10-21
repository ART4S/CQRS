using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;

namespace WebFeatures.Application.Features.Blogs.CreateBlog
{
    public class CreateBlogCommand : ICommand<Unit>
    {
        public int AuthorId { get; set; }
        public string Title { get; set; }
    }
}
