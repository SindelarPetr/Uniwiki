using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostFileDownloadModelMap : ModelMapBase<PostFileDownloadModel, Guid>
    {
        public PostFileDownloadModelMap() : base("PostFileDownloads")
        {
        }

        public override void Map(EntityTypeBuilder<PostFileDownloadModel> builder)
        {
            base.Map(builder);

            builder.HasOne(m => m.Token).WithMany().HasForeignKey(m => m.TokenId);
            builder.HasOne(m => m.FileDownloaded).WithMany().HasForeignKey(m => m.FileDownloadedId);
        }
    }
}