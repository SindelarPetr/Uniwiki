using System;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostFileModelMap : ModelMapBase<PostFileModel, Guid>
    {
        public PostFileModelMap() : base("PostFiles")
        {
        }
    }
}