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
        

        internal PostFileDownloadModel(Guid id, Guid tokenId, Guid fileDownloadedId, DateTime downloadTime)
            : base(id)
        {
            TokenId = tokenId;
            FileDownloadedId = fileDownloadedId;
            DownloadTime = downloadTime;
        }

        protected PostFileDownloadModel()
        {

        }
    }
}