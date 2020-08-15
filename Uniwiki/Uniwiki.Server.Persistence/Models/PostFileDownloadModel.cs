using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostFileDownloadModel : ModelBase<Guid>
    {
        public Guid TokenId { get; protected set; }
        public LoginTokenModel Token { get; protected set; } = null!;
        public Guid FileDownloadedId { get; protected set; }
        public PostFileModel FileDownloaded { get; protected set; } = null!;
        public DateTime DownloadTime { get; protected set; }
        

        internal PostFileDownloadModel(Guid id, LoginTokenModel token, PostFileModel fileDownloaded, DateTime downloadTime)
            : base(id)
        {
            TokenId = token.Id;
            Token = token;
            FileDownloadedId = fileDownloaded.Id;
            FileDownloaded = fileDownloaded;
            DownloadTime = downloadTime;
        }

        protected PostFileDownloadModel()
        {

        }
    }
}