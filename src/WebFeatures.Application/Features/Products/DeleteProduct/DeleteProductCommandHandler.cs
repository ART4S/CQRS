using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Features.Products.Events;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Events;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Products.DeleteProduct
{
    internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Empty>
    {
        private readonly IWriteDbContext _db;
        private readonly IEventMediator _events;

        public DeleteProductCommandHandler(IWriteDbContext db, IEventMediator events)
        {
            _db = db;
            _events = events;
        }

        public async Task<Empty> HandleAsync(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (!await _db.Products.ExistsAsync(request.Id))
            {
                throw new ValidationException("Product doesn't exist");
            }

            await _db.Products.DeleteAsync(request.Id);

            await _events.PublishAsync(new ProductDeleted(request.Id));

            return Empty.Value;
        }
    }
}