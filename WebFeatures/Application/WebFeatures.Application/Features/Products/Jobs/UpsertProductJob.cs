using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities;
using WebFeatures.HangfireJobs;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
    public class UpsertProductJobArg
    {
        public Guid ProductId { get; }
        public UpsertProductJobArg(Guid productId) => ProductId = productId;
    }

    public class UpsertProductJob : IBackgroundJob<UpsertProductJobArg>
    {
        private readonly IWriteContext _writeDb;
        private readonly IReadContext _readDb;

        public UpsertProductJob(IWriteContext writeDb, IReadContext readDb)
        {
            _writeDb = writeDb;
            _readDb = readDb;
        }

        public async Task ExecuteAsync(UpsertProductJobArg argument)
        {
            Product product = await _writeDb.Products
                .Include(x => x.Picture)
                .Include(x => x.Manufacturer)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .FirstAsync(x => x.Id == argument.ProductId);

            await _readDb.UpsertAsync(product);
        }
    }
}
