using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductComments.Events;
using WebFeatures.Application.Features.ProductComments.Requests.Commands;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.ProductComments.Handlers
{
    internal class ProductCommentCommandHandler : IRequestHandler<CreateProductComment, Guid>
    {
        private readonly IWriteDbContext _db;
        private readonly ICurrentUserService _currentUser;
        private readonly IEventStorage _events;
        private readonly IMapper _mapper;

        public ProductCommentCommandHandler(
            IWriteDbContext db,
            ICurrentUserService currentUser,
            IEventStorage events,
            IMapper mapper)
        {
            _db = db;
            _currentUser = currentUser;
            _events = events;
            _mapper = mapper;
        }

        public async Task<Guid> HandleAsync(CreateProductComment request, CancellationToken cancellationToken)
        {
            ProductComment comment = _mapper.Map<ProductComment>(request);
            comment.AuthorId = _currentUser.UserId;

            await _db.ProductComments.CreateAsync(comment);

            await _events.AddAsync(new ProductCommentCreated(comment.Id));

            return comment.Id;
        }
    }
}
