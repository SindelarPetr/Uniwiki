using Server.Appliaction.ServerActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniwiki.Server.Application.RequestResponse;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.RepositoryAbstractions;

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
}
