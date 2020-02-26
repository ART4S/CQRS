using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.Data;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.DeleteBlog
{
    public class DeleteBlogCommandHandler : ICommandHandler<DeleteBlogCommand, Unit>
    {
        private readonly IRepository<Blog, int> _blogRepo;

        public DeleteBlogCommandHandler(IRepository<Blog, int> blogRepo)
        {
            _blogRepo = blogRepo;
        }

        public Unit Handle(DeleteBlogCommand input)
        {
            _blogRepo.Remove(input.Id);
            _blogRepo.SaveChanges();

            return Unit.Value;
        }
    }
}
