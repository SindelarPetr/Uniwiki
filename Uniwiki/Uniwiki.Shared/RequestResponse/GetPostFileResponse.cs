using System.IO;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetPostFileResponse : ResponseBase
    {
        public FileStream FileStream { get; }
        public string OriginalName { get; }

        public GetPostFileResponse(FileStream fileStream, string originalName)
        {
            FileStream = fileStream;
            OriginalName = originalName;
        }
    }
}