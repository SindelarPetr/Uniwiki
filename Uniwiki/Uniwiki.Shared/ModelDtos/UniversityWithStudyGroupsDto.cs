using System;

namespace Uniwiki.Shared.ModelDtos
{
    public class StudyGroupToSelectDto
    {
        public string UniversityShortName { get; }
        public Guid UniversityId { get; }
        public string StudyGroupLongName { get; }
        public string StudyGroupShortName { get; }
        public Guid StudyGroupId { get; }

        public StudyGroupToSelectDto(string studyGroupLongName, string studyGroupShortName, Guid studyGroupId, string universityShortName, Guid universityId)
        {
            StudyGroupLongName = studyGroupLongName;
            StudyGroupShortName = studyGroupShortName;
            StudyGroupId = studyGroupId;
            UniversityShortName = universityShortName;
            UniversityId = universityId;
        }
    }
}
