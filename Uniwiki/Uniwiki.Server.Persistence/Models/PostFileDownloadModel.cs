using System;
using Toolbelt.ComponentModel.DataAnnotations.Schema.V5;
using Uniwiki.Server.Persistence.Models.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostFileDownloadModel : ModelBase<Guid>
    {
        public Guid TokenId { get; set; }
        public LoginTokenModel Token { get; set; } = null!;
        public Guid FileDownloadedId { get; set; }
        public PostFileModel FileDownloaded { get; set; } = null!;
        [IndexColumn]
        public DateTime DownloadTime { get; set; }
        

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