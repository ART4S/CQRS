using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Application.Jobs.Common;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Jobs
{
    public class UpsertReviewJobArg
    {
        public Guid ReviewId { get; }
        public UpsertReviewJobArg(Guid reviewId) => ReviewId = reviewId;
    }

    public class UpsertReviewJob : SyncEntityBetweenDatabasesJob<Review>
    {
        public UpsertReviewJob(IWriteContext writeDb, IReadContext readDb) : base(writeDb, readDb)
        {
        }

        public override async Task ExecuteAsync(SyncEntityBetweenDatabasesJobArg<Review> argument)
        {
            Review review = await WriteDb.Reviews
                .Include(x => x.Author)
                .FirstAsync(x => x.Id == argument.Id);

            await ReadDb.UpsertAsync(review);
        }
    }
}
