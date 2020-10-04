namespace Uniwiki.Server.WebHost.Controllers
{
    public class CourseViewModel
    {
        public CourseViewModel(string univeristyShortName, string univeristyLongName, string facultyShortName, string facultyLongName, string courseName, string courseCode, PostItemViewModel[] postItems)
        {
            UniveristyShortName = univeristyShortName;
            UniveristyLongName = univeristyLongName;
            FacultyShortName = facultyShortName;
            FacultyLongName = facultyLongName;
            CourseName = courseName;
            CourseCode = courseCode;
            PostItems = postItems;
        }

        public string UniveristyShortName { get; }
        public string UniveristyLongName { get; }
        public string FacultyShortName { get; }
        public string FacultyLongName { get; }
        public string CourseName { get; }
        public string CourseCode { get; }
        public PostItemViewModel[] PostItems { get; }
    }
}