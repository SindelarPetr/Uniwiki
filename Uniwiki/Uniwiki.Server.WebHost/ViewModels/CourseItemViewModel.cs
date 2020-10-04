namespace Uniwiki.Server.WebHost.Controllers
{
    public class CourseItemViewModel
    {
        public CourseItemViewModel(string name, string code, string url)
        {
            Name = name;
            Code = code;
            Url = url;
        }

        public string Name { get; }
        public string Code { get; }
        public string Url { get; }
    }
}