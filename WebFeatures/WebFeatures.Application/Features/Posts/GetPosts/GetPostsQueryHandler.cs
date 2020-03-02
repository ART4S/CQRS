using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.GetPosts
{
    public class GetPostsQueryHandler : IQueryHandler<GetPostsQuery, IQueryable<PostInfoDto>>
    {
        private readonly IRepository<Post, int> _postRepo;
        private readonly IMapper _mapper;

        public GetPostsQueryHandler(
            IRepository<Post, int> postRepo, 
            IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        public IQueryable<PostInfoDto> Handle(GetPostsQuery request)
        {
            var posts = _postRepo.GetAll()
                .ProjectTo<PostInfoDto>(_mapper.ConfigurationProvider);

            return posts;
        }
    }
}
