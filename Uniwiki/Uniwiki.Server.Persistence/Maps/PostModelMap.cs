using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostModelMap : RemovableModelMapBase<PostModel, Guid>
    {
        public PostModelMap() : base("Posts")
        {
        }
    }
}
