using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostCommentModelMap : RemovableModelMapBase<PostCommentModel, Guid>
    {
        public PostCommentModelMap() : base("PostComments")
        {

        }

        public override void Map(EntityTypeBuilder<PostCommentModel> builder)
        {
            base.Map(builder);


        }
    }
}