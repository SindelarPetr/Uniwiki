namespace Uniwiki.Server.WebHost.Controllers
{
    public class FacultyViewModel
    {
        public FacultyViewModel(string universityShortName, string universityLongName, string shortName, string longName, CourseItemViewModel[] courses)
        {
            UniversityShortName = universityShortName;
            UniversityLongName = universityLongName;
            ShortName = shortName;
            LongName = longName;
            Courses = courses;
        }

        public string UniversityShortName { get; }
        public string UniversityLongName { get; }
        public string ShortName { get; }
        public string LongName { get; }
        public CourseItemViewModel[] Courses { get; }
    }
}