using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.DeletePost
{
    public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand, Unit>
    {
        private readonly IRepository<Post, int> _postRepo;

        public DeletePostCommandHandler(IRepository<Post, int> postRepo)
        {
            _postRepo = postRepo;
        }

        public Unit Handle(DeletePostCommand request)
        {
            _postRepo.Remove(request.Id);
            _postRepo.SaveChanges();

            return Unit.Value;
        }
    }
}
