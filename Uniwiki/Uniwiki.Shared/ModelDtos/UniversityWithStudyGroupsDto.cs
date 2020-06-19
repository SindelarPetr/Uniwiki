namespace Uniwiki.Shared.ModelDtos
{
    public class UniversityWithStudyGroupsDto
    {
        public UniversityDto University { get; set; }
        public StudyGroupDto[] StudyGroups { get; set; }

        public UniversityWithStudyGroupsDto(UniversityDto university, StudyGroupDto[] studyGroups)
        {
            University = university;
            StudyGroups = studyGroups;
        }
    }
}
