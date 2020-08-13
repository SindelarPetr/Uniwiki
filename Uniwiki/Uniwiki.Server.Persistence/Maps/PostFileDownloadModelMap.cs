using System;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostFileDownloadModelMap : ModelMapBase<PostFileDownloadModel, Guid>
    {
        public LoginTokenModel Token { get; protected set; }
        public PostFileModel FileDownloaded { get; protected set; }
        public DateTime DownloadTime { get; protected set; }
        

        internal PostFileDownloadModel(Guid id, LoginTokenModel token, PostFileModel fileDownloaded, DateTime downloadTime)
            : base(id)
        {
            Id = id;
            Token = token;
            FileDownloaded = fileDownloaded;
            DownloadTime = downloadTime;
        }

        protected PostFileDownloadModel()
        {

        }
    }
}