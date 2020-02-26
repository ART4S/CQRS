using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces.Data;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.GetBlogsInfo
{
    public class GetBlogInfosQueryHandler : IQueryHandler<GetBlogInfosQuery, IQueryable<BlogInfoDto>>
    {
        private readonly IRepository<Blog, int> _blogRepo;
        private readonly IMapper _mapper;

        public GetBlogInfosQueryHandler(
            IRepository<Blog, int> blogRepo, 
            IMapper mapper)
        {
            _blogRepo = blogRepo;
            _mapper = mapper;
        }

        public IQueryable<BlogInfoDto> Handle(GetBlogInfosQuery input)
        {
            var blogs = _blogRepo.GetAllAsQuery()
                .ProjectTo<BlogInfoDto>(_mapper.ConfigurationProvider);

            return blogs;
        }
    }
}
