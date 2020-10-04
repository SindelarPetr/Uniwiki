namespace Uniwiki.Server.WebHost.Controllers
{
    public class PostFileItemViewModel
    {
        public PostFileItemViewModel(string fileName, string size)
        {
            FileName = fileName;
            Size = size;
        }

        public string FileName { get; }
        public string Size { get; }
    }
}