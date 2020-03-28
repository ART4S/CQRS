using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Common;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Comments.CreateComment
{
    public class CreateCommentHandler : IRequestHandler<CreateComment, Empty>
    {
        private readonly IWebFeaturesDbContext _db;
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTime _dateTime;

        public CreateCommentHandler(
            IWebFeaturesDbContext db,
            ICurrentUserService currentUser,
            IDateTime dateTime)
        {
            _db = db;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        public async Task<Empty> HandleAsync(CreateComment request, CancellationToken cancellationToken)
        {
            var comment = new UserComment(
                request.ProductId,
                _currentUser.UserId,
                request.Body,
                _dateTime.Now,
                request.ParentCommentId);

            await _db.UserComments.AddAsync(comment, cancellationToken);

            return Empty.Value;
        }
    }
}
