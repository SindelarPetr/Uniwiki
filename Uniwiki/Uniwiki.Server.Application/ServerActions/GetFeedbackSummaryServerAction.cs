using Server.Appliaction.ServerActions;
using Shared.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class GetFeedbackSummaryServerAction : ServerActionBase<GetFeedbackSummaryRequestDto, GetFeedbackSummaryResponseDto>
    {
        private readonly IFeedbackRepository _feedbackRepository;

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        public GetFeedbackSummaryServerAction(IServiceProvider serviceProvider, IFeedbackRepository feedbackRepository) : base(serviceProvider)
        {
            _feedbackRepository = feedbackRepository;
        }

        protected override Task<GetFeedbackSummaryResponseDto> ExecuteAsync(GetFeedbackSummaryRequestDto request, RequestContext context)
        {
            var averageRating = _feedbackRepository.GetAverageRating();
            var feedbacksCount = _feedbackRepository.GetFeedbacksCount();
            var lastFeedbacks = _feedbackRepository.GetLastFeedbacks(request.FeedbacksCount).ToArray();
            
            var textOnlyFeedbacksCount = _feedbackRepository.GetTextOnlyFeedbacksCount();
            var textOnlyFeedbacksCountPercentage = Math.Round(textOnlyFeedbacksCount / (double)feedbacksCount * 100);
            
            var ratingOnlyFeedbacksCount = _feedbackRepository.RatingOnlyFeedbacksCount();
            var ratingOnlyFeedbacksCountPercentage = Math.Round(ratingOnlyFeedbacksCount / (double)feedbacksCount * 100);
            
            var textAndRatingFeedbacksCount = _feedbackRepository.TextAndRatingFeedbacksCount();
            var textAndRatingFeedbacksCountPercentage = Math.Round(textAndRatingFeedbacksCount / (double)feedbacksCount * 100);

            var response = new GetFeedbackSummaryResponseDto(averageRating, feedbacksCount, lastFeedbacks, textOnlyFeedbacksCount, ratingOnlyFeedbacksCount, textAndRatingFeedbacksCount, textAndRatingFeedbacksCountPercentage, ratingOnlyFeedbacksCountPercentage, textOnlyFeedbacksCountPercentage);

            return Task.FromResult(response);
        }
    }

    public class GetFeedbackSummaryRequestDto : RequestBase<GetFeedbackSummaryResponseDto>
    {

        public GetFeedbackSummaryRequestDto(int feedbacksCount)
        {
            FeedbacksCount = feedbacksCount;
        }

        public int FeedbacksCount { get; }
    }

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
