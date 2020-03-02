using AutoMapper;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.CreatePost
{
    public class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, Unit>
    {
        private readonly IRepository<Post, int> _postRepo;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(
            IRepository<Post, int> postRepo, 
            IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        public Unit Handle(CreatePostCommand request)
        {
            var post = _mapper.Map<Post>(request);

            _postRepo.Add(post);
            _postRepo.SaveChanges();

            return Unit.Value;
        }
    }
}
