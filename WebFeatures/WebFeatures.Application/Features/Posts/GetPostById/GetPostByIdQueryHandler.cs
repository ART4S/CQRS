using AutoMapper;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.GetPostById
{
    public class GetPostByIdQueryHandler : IQueryHandler<GetPostByIdQuery, PostEditDto>
    {
        private readonly IRepository<Post, int> _postRepo;
        private readonly IMapper _mapper;

        public GetPostByIdQueryHandler(
            IRepository<Post, int> postRepo, 
            IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        public PostEditDto Handle(GetPostByIdQuery request)
        {
            var post = _postRepo.GetById(request.Id);
            var postDto = _mapper.Map<PostEditDto>(post);

            return postDto;
        }
    }
}
