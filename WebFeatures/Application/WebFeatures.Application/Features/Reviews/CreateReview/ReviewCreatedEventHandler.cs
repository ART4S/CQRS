using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Jobs;
using WebFeatures.Application.Jobs;
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
            return _jobManager.EnqueueAsync(new UpsertReviewJobArg(eve.ReviewId));
        }
    }
}
