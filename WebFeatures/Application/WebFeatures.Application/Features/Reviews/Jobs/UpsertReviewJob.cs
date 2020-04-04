using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities;
using WebFeatures.HangfireJobs;

namespace WebFeatures.Application.Features.Reviews.Jobs
{
    public class UpsertReviewJobArg
    {
        public Guid ReviewId { get; }
        public UpsertReviewJobArg(Guid reviewId) => ReviewId = reviewId;
    }

    public class UpsertReviewJob : IBackgroundJob<UpsertReviewJobArg>
    {
        private readonly IWriteContext _writeDb;
        private readonly IReadContext _readDb;

        public UpsertReviewJob(IWriteContext writeDb, IReadContext readDb)
        {
            _writeDb = writeDb;
            _readDb = readDb;
        }

        public async Task ExecuteAsync(UpsertReviewJobArg argument)
        {
            Review review = await _writeDb.Reviews
                .Include(x => x.Author)
                .FirstAsync(x => x.Id == argument.ReviewId);

            await _readDb.UpsertAsync(review);
        }
    }
}
