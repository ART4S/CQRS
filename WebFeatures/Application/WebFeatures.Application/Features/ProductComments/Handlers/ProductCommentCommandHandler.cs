using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductComments.Events;
using WebFeatures.Application.Features.ProductComments.Requests.Commands;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Domian.Entities.Products;

namespace WebFeatures.Application.Features.ProductComments.Handlers
{
    internal class ProductCommentCommandHandler : IRequestHandler<CreateProductComment, Guid>
    {
        private readonly IWriteDbContext _db;
        private readonly ICurrentUserService _currentUser;
        private readonly IEventMediator _events;
        private readonly IMapper _mapper;

        public ProductCommentCommandHandler(
            IWriteDbContext db,
            ICurrentUserService currentUser,
            IEventMediator events,
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

            await _events.PublishAsync(new ProductCommentCreated(comment.Id));

            return comment.Id;
        }
    }
}