using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.GetBlogs
{
    public class GetBlogsQueryHandler : IQueryHandler<GetBlogsQuery, IQueryable<BlogInfoDto>>
    {
        private readonly IRepository<Blog, int> _blogRepo;
        private readonly IMapper _mapper;

        public GetBlogsQueryHandler(
            IRepository<Blog, int> blogRepo, 
            IMapper mapper)
        {
            _blogRepo = blogRepo;
            _mapper = mapper;
        }

        public IQueryable<BlogInfoDto> Handle(GetBlogsQuery request)
        {
            var blogs = _blogRepo.GetAll()
                .ProjectTo<BlogInfoDto>(_mapper.ConfigurationProvider);

            return blogs;
        }
    }
}
