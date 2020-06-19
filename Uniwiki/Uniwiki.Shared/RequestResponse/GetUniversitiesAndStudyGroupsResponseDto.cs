using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetUniversitiesAndStudyGroupsResponseDto : ResponseBase
    {
        public UniversityWithStudyGroupsDto[] UniversitiesWithStudyGroups { get; set; }

        public GetUniversitiesAndStudyGroupsResponseDto(UniversityWithStudyGroupsDto[] universitiesWithStudyGroups)
        {
            UniversitiesWithStudyGroups = universitiesWithStudyGroups;
        }
    }
}