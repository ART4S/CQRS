using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.EditProduct
{
    public class EditProductHandler : IRequestHandler<EditProduct, Empty>
    {
        private readonly IWriteContext _db;
        private readonly IMapper _mapper;

        public EditProductHandler(IWriteContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Empty> HandleAsync(EditProduct request, CancellationToken cancellationToken)
        {
            Product product = await _db.Products.FindAsync(request.Id);
            _mapper.Map(request, product);

            product.Events.Add(new ProductEdited(product.Id));

            return Empty.Value;
        }
    }
}
