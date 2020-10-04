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
}