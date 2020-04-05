using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Application.Jobs.Common;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Jobs
{
    public class UpsertCommentJob : SyncEntityBetweenDatabasesJob<UserComment>
    {
        public UpsertCommentJob(IWriteContext writeDb, IReadContext readDb) : base(writeDb, readDb)
        {
        }

        public override async Task ExecuteAsync(SyncEntityBetweenDatabasesJobArg<UserComment> argument)
        {
            UserComment comment = await WriteDb.UserComments
                .Include(x => x.Author)
                .Include(x => x.Product)
                .FirstAsync(x => x.Id == argument.Id);

            await ReadDb.UpsertAsync(comment);
        }
    }
}
