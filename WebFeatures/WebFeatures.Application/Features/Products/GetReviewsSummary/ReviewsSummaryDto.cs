namespace WebFeatures.Application.Features.Products.GetReviewsSummary
{
    public class ReviewsSummaryDto
    {
        public double RatingAverage { get; set; }
        public int ReviewsCount { get; set; }
        public int OneStarsCount { get; set; }
        public int TwoStarsCount { get; set; }
        public int ThreeStarsCount { get; set; }
        public int FourStarsCount { get; set; }
        public int FiveStarsCount { get; set; }
    }
}
