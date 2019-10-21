using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.DeleteBlog
{
    public class DeleteBlogCommandHandler : ICommandHandler<DeleteBlogCommand, Unit>
    {
        private readonly IAppContext _context;

        public DeleteBlogCommandHandler(IAppContext context)
        {
            _context = context;
        }

        public Unit Handle(DeleteBlogCommand input)
        {
            _context.Remove<Blog>(input.Id);
            return Unit.Value;
        }
    }
}
