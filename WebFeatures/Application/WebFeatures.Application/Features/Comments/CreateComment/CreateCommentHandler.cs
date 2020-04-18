using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Comments.CreateComment
{
    internal class CreateCommentHandler : IRequestHandler<CreateComment, Guid>
    {
        private readonly IDbContext _db;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public CreateCommentHandler(
            IDbContext db,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _db = db;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<Guid> HandleAsync(CreateComment request, CancellationToken cancellationToken)
        {
            UserComment comment = _mapper.Map<UserComment>(request);
            comment.AuthorId = _currentUser.UserId;

            await _db.UserComments.AddAsync(comment, cancellationToken);

            return comment.Id;
        }
    }
}
