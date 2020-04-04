using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Reviews.Jobs;
using WebFeatures.Events;
using WebFeatures.HangfireJobs;

namespace WebFeatures.Application.Features.Reviews.Requests.CreateReview
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
