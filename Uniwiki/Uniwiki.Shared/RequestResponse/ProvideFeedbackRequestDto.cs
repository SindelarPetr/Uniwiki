using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class ProvideFeedbackRequestDto : RequestBase<ProvideFeedbackResponseDto>
    {
        public ProvideFeedbackRequestDto(int? rating, string text)
        {
            Rating = rating;
            Text = text;
        }

        public int? Rating { get; set; }
        public string Text { get; set; }
    }
}
