using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Maps.Base
{
    public abstract class RemovableModelMapBase<TModel, TId> : ModelMapBase<TModel, TId> 
        where TModel : RemovableModelBase<TId>
        where TId : struct
    {
        public RemovableModelMapBase(string tableName)
            : base(tableName)
        {

        }
        public override void Map(EntityTypeBuilder<TModel> builder)
        {
            builder.HasQueryFilter(m => !m.IsRemoved);
        }
    }
}
