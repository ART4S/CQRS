using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.ProductComments.CreateProductComment
{
    internal class CreateProductCommentHandler : IRequestHandler<CreateProductComment, Guid>
    {
        private readonly IDbContext _db;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public CreateProductCommentHandler(
            IDbContext db,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _db = db;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<Guid> HandleAsync(CreateProductComment request, CancellationToken cancellationToken)
        {
            ProductComment comment = _mapper.Map<ProductComment>(request);
            comment.AuthorId = _currentUser.UserId;

            await _db.ProductComments.CreateAsync(comment);

            return comment.Id;
        }
    }
}
