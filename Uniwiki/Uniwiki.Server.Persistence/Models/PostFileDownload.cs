using System;
using Uniwiki.Server.Persistence.Models.Authentication;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostFileDownload
    {
        public LoginTokenModel Token { get; }
        public PostFileModel FileDownloaded { get; }
        public DateTime DownloadTime { get; }

        public PostFileDownload(LoginTokenModel token, PostFileModel fileDownloaded, DateTime downloadTime)
        {
            Token = token;
            FileDownloaded = fileDownloaded;
            DownloadTime = downloadTime;
        }
    }
}