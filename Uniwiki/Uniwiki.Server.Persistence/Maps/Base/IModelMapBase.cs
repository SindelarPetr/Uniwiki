using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Uniwiki.Server.Persistence.Maps.Base
{
    public abstract class IModelMapBase
    {
        public abstract void Map(ModelBuilder builder);
    }
}
