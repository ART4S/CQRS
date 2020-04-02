using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Common;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Comments.CreateComment
{
    public class CreateCommentHandler : IRequestHandler<CreateComment, Guid>
    {
        private readonly IWriteContext _db;
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTime _dateTime;

        public CreateCommentHandler(
            IWriteContext db,
            ICurrentUserService currentUser,
            IDateTime dateTime)
        {
            _db = db;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        public async Task<Guid> HandleAsync(CreateComment request, CancellationToken cancellationToken)
        {
            var comment = new UserComment()
            {
                ProductId = request.ProductId,
                AuthorId = _currentUser.UserId,
                Body = request.Body,
                CreatedAt = _dateTime.Now,
                ParentCommentId = request.ParentCommentId
            };

            await _db.UserComments.AddAsync(comment, cancellationToken);

            return comment.Id;
        }
    }
}
