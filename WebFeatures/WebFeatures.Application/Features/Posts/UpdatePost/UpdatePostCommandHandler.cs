using AutoMapper;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.UpdatePost
{
    public class UpdatePostCommandHandler : ICommandHandler<UpdatePostCommand, Unit>
    {
        private readonly IRepository<Post, int> _postRepo;
        private readonly IMapper _mapper;

        public UpdatePostCommandHandler(
            IRepository<Post, int> postRepo, 
            IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        public Unit Handle(UpdatePostCommand request)
        {
            var post = _postRepo.GetById(request.Id);
            _mapper.Map(request, post);

            _postRepo.SaveChanges();

            return Unit.Value;
        }
    }
}
