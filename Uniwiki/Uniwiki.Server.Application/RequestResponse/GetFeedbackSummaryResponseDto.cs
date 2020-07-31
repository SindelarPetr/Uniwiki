using Shared.RequestResponse;

namespace Uniwiki.Server.Application.RequestResponse
{
    public class GetFeedbackSummaryResponseDto : ResponseBase
    {
        public double? AverageRating { get; }
        public int FeedbackCount { get; }
        public string[] Feedbacks { get; }
        public int TextOnlyFeedbacksCount { get; }
        public int RatingOnlyFeedbacksCount { get; }
        public int TextAndRatingFeedbacksCount { get; }
        public double TextAndRatingFeedbacksCountPercentage { get; }
        public double RatingOnlyFeedbacksCountPercentage { get; }
        public double TextOnlyFeedbacksCountPercentage { get; }

        public GetFeedbackSummaryResponseDto(double? averageRating, int ratingsCount, string[] feedbacks, int textOnlyFeedbacksCount, int ratingOnlyFeedbacksCount, int textAndRatingFeedbacksCount, double textAndRatingFeedbacksCountPercentage, double ratingOnlyFeedbacksCountPercentage, double textOnlyFeedbacksCountPercentage)
        {
            AverageRating = averageRating;
            FeedbackCount = ratingsCount;
            Feedbacks = feedbacks;
            TextOnlyFeedbacksCount = textOnlyFeedbacksCount;
            RatingOnlyFeedbacksCount = ratingOnlyFeedbacksCount;
            TextAndRatingFeedbacksCount = textAndRatingFeedbacksCount;
            TextAndRatingFeedbacksCountPercentage = textAndRatingFeedbacksCountPercentage;
            RatingOnlyFeedbacksCountPercentage = ratingOnlyFeedbacksCountPercentage;
            TextOnlyFeedbacksCountPercentage = textOnlyFeedbacksCountPercentage;
        }
    }
}
