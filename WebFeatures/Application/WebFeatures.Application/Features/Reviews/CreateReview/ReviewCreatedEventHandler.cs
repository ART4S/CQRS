using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Jobs;
using WebFeatures.Application.Jobs.Common;
using WebFeatures.Domian.Entities;
using WebFeatures.Events;

namespace WebFeatures.Application.Features.Reviews.CreateReview
{
    public class ReviewCreatedEventHandler : IEventHandler<ReviewCreatedEvent>
    {
        private readonly IBackgroundJobManager _jobManager;

        public ReviewCreatedEventHandler(IBackgroundJobManager jobManager)
        {
            _jobManager = jobManager;
        }

        public Task HandleAsync(ReviewCreatedEvent eve, CancellationToken cancellationToken)
        {
            return _jobManager.EnqueueAsync(new SyncEntityBetweenDatabasesJobArg<Review>(eve.ReviewId));
        }
    }
}
