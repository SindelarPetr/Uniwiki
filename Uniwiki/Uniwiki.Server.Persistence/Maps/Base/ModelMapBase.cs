using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Maps.Base
{

    public abstract class ModelMapBase<TModel, TId> : IModelMapBase  where TModel : ModelBase<TId>
    {
        private readonly string _tableName;

        public ModelMapBase(string tableName)
        {
            _tableName = tableName;
        }

        public sealed override void Map(ModelBuilder builder)
        {
            Map(builder.Entity<TModel>());
        }

        public virtual void Map(EntityTypeBuilder<TModel> builder)
        {
            builder.HasKey(m => m.Id);
            builder.ToTable(_tableName);
        }
    }
}
