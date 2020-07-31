using Shared.RequestResponse;

namespace Uniwiki.Server.Application.RequestResponse
{
    public class GetFeedbackSummaryRequestDto : RequestBase<GetFeedbackSummaryResponseDto>
    {

        public GetFeedbackSummaryRequestDto(int feedbacksCount)
        {
            FeedbacksCount = feedbacksCount;
        }

        public int FeedbacksCount { get; }
    }
}
