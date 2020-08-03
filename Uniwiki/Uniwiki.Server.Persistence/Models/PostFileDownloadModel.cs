using System;
using Uniwiki.Server.Persistence.Models.Authentication;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostFileDownloadModel
    {
        public LoginTokenModel Token { get; protected set; }
        public PostFileModel FileDownloaded { get; protected set; }
        public DateTime DownloadTime { get; protected set; }

        internal PostFileDownloadModel()
        {

        }

        internal PostFileDownloadModel(LoginTokenModel token, PostFileModel fileDownloaded, DateTime downloadTime)
        {
            Token = token;
            FileDownloaded = fileDownloaded;
            DownloadTime = downloadTime;
        }
    }
}