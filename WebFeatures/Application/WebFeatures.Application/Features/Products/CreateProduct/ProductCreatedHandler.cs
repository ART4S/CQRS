using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Domian.Entities;
using WebFeatures.Events;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
    public class ProductCreatedHandler : IEventHandler<ProductCreated>
    {
        private readonly IWriteContext _writeContext;
        private readonly IReadContext _readContext;

        public ProductCreatedHandler(IWriteContext writeContext, IReadContext readContext)
        {
            _writeContext = writeContext;
            _readContext = readContext;
        }

        public async Task HandleAsync(ProductCreated eve, CancellationToken cancellationToken)
        {
            Product product = await _writeContext.Products.FindAsync(eve.ProductId);
            await _readContext.AddAsync(product);
        }
    }
}
