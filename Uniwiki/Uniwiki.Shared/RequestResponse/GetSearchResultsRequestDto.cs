using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetSearchResultsRequestDto : RequestBase<GetSearchResultsResponseDto>
    {
        public string SearchedText { get; set; }
        public Guid? UniversityId { get; set; }
        public Guid? StudyGroupId { get; set; }

        public GetSearchResultsRequestDto(string searchedText, Guid? universityId, Guid? studyGroupId)
        {
            SearchedText = searchedText;
            UniversityId = universityId;
            StudyGroupId = studyGroupId;
        }
    }
}