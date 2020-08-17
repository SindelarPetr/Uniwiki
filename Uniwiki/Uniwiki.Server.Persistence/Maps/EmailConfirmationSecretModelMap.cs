using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class EmailConfirmationSecretModelMap : ModelMapBase<EmailConfirmationSecretModel, Guid>
    {
        public EmailConfirmationSecretModelMap() : base("EmailConfirmationSecrets")
        {

        }

        public override void Map(EntityTypeBuilder<EmailConfirmationSecretModel> builder)
        {
            base.Map(builder);

            builder
                .HasOne(m => m.Profile)
                .WithMany()
                .HasForeignKey(m => m.ProfileId);
        }
    }
}