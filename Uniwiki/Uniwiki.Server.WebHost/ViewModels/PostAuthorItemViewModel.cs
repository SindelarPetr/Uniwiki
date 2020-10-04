namespace Uniwiki.Server.WebHost.Controllers
{
    public class PostAuthorItemViewModel
    {
        public PostAuthorItemViewModel(string nameAndSurname, string url)
        {
            NameAndSurname = nameAndSurname;
            Url = url;
        }

        public string NameAndSurname { get; }
        public string Url { get; }
    }
}