using System;
using Shared;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class AddStudyGroupRequestDto : RequestBase<AddStudyGroupResponseDto>
    {
        public string StudyGroupName { get; set; }
        public string StudyGroupShortcut { get; set; }
        public Guid UniversityId { get; set; }
        public Language PrimaryLanguage { get; set; }

        public AddStudyGroupRequestDto(string studyGroupName, string studyGroupShortcut, Guid universityId, Language primaryLanguage)
        {
            StudyGroupName = studyGroupName;
            StudyGroupShortcut = studyGroupShortcut;
            UniversityId = universityId;
            PrimaryLanguage = primaryLanguage;
        }
    }
}