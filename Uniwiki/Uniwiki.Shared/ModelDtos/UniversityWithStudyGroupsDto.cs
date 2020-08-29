using System;

namespace Uniwiki.Shared.ModelDtos
{
    public class UniversityToSelectDto
    {
        public string UniversityShortName { get; }
        public string UniversityLongName { get; }
        public StudyGroupToSelectDto[] StudyGroupsToSelect { get; }

        public UniversityToSelectDto(string universityShortName, string universityLongName, StudyGroupToSelectDto[] studyGroupsToSelect)
        {
            UniversityShortName = universityShortName;
            UniversityLongName = universityLongName;
            StudyGroupsToSelect = studyGroupsToSelect;
        }
    }

    public class StudyGroupToSelectDto
    {
        public string UniversityShortName { get; }
        public string StudyGroupLongName { get; }
        public string StudyGroupShortName { get; }
        public Guid StudyGroupId { get; }

        public StudyGroupToSelectDto(string studyGroupLongName, string studyGroupShortName, Guid studyGroupId, string universityShortName)
        {
            StudyGroupLongName = studyGroupLongName;
            StudyGroupShortName = studyGroupShortName;
            StudyGroupId = studyGroupId;
            UniversityShortName = universityShortName;
        }
    }
}
