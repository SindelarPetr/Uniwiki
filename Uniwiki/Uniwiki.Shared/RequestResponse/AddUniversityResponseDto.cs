using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class AddUniversityResponseDto : ResponseBase
    {
        public AddUniversityResponseDto(UniversityDto university)
        {
            University = university;
        }

        public UniversityDto University { get; }
    }
}
