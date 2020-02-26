using AutoMapper;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.Data;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.CreateBlog
{
    public class CreateBlogCommandHandler : ICommandHandler<CreateBlogCommand, Unit>
    {
        private readonly IRepository<Blog, int> _blogRepo;
        private readonly IMapper _mapper;

        public CreateBlogCommandHandler(
            IRepository<Blog, int> blogRepo, 
            IMapper mapper)
        {
            _blogRepo = blogRepo;
            _mapper = mapper;
        }

        public Unit Handle(CreateBlogCommand input)
        {
            var blog = _mapper.Map<Blog>(input);

            _blogRepo.Add(blog);
            _blogRepo.SaveChanges();

            return Unit.Value;
        }
    }
}
