using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class LoginTokenModelMap : ModelMapBase<LoginTokenModel, Guid>
    {

        public LoginTokenModelMap() : base("LoginTokens") 
        {         
        
        }

        public override void Map(EntityTypeBuilder<LoginTokenModel> builder)
        {
            base.Map(builder);

            builder
                .HasOne(m => m.Profile)
                .WithMany()
                .HasForeignKey(m => m.ProfileId);
        }
    }
}