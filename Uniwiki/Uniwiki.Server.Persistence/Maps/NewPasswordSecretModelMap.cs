using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class NewPasswordSecretModelMap : ModelMapBase<NewPasswordSecretModel, Guid>
    {
        public NewPasswordSecretModelMap() : base("NewPasswordSecrets")
        {
        }

        public override void Map(EntityTypeBuilder<NewPasswordSecretModel> builder)
        {
            base.Map(builder);

            builder
                .HasOne(m => m.Profile)
                .WithMany()
                .HasForeignKey(m => m.ProfileId);
        }
    }
}