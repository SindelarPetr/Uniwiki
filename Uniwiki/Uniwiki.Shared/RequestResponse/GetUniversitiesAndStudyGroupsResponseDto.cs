using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetUniversitiesAndStudyGroupsResponseDto : ResponseBase
    {
        public UniversityToSelectDto[] UniversitiesWithStudyGroups { get; set; }

        public GetUniversitiesAndStudyGroupsResponseDto(UniversityToSelectDto[] universitiesWithStudyGroups)
        {
            UniversitiesWithStudyGroups = universitiesWithStudyGroups;
        }
    }
}