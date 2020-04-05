using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Application.Jobs.Common;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Jobs
{
    public class UpsertProductJob : SyncEntityBetweenDatabasesJob<Product>
    {
        public UpsertProductJob(IWriteContext writeDb, IReadContext readDb) : base(writeDb, readDb)
        {
        }

        public override async Task ExecuteAsync(SyncEntityBetweenDatabasesJobArg<Product> argument)
        {
            Product product = await WriteDb.Products
                .Include(x => x.Picture)
                .Include(x => x.Manufacturer)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .FirstAsync(x => x.Id == argument.Id);

            await ReadDb.UpsertAsync(product);
        }
    }
}
