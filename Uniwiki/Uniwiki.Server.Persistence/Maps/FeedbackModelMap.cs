using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class FeedbackModelMap : RemovableModelMapBase<FeedbackModel, Guid>
    {
        public FeedbackModelMap() : base("Feedbacks")
        {
        }

        public override void Map(EntityTypeBuilder<FeedbackModel> builder)
        {
            base.Map(builder);

            builder
                .HasOne(m => m.User)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(m => m.UserId);
        }
    }
}
