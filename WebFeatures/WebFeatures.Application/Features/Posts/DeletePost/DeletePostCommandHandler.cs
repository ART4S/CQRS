using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.DeletePost
{
    public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand, Unit>
    {
        private readonly IAppContext _context;

        public DeletePostCommandHandler(IAppContext context)
        {
            _context = context;
        }

        public Unit Handle(DeletePostCommand input)
        {
            _context.Remove<Post>(input.Id);
            return Unit.Value;
        }
    }
}
