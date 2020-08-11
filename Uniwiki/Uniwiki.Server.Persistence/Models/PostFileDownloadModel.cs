using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostFileDownloadModel : IIdModel<Guid>, IRemovableModel
    {
        public LoginTokenModel Token { get; protected set; }
        public PostFileModel FileDownloaded { get; protected set; }
        public DateTime DownloadTime { get; protected set; }
        public Guid Id { get; protected set; }

        bool IRemovableModel.IsRemoved { get; set; }

        public PostFileDownloadModel(Guid id, LoginTokenModel token, PostFileModel fileDownloaded, DateTime downloadTime)
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