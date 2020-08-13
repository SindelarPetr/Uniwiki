using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Maps.Base
{
    public abstract class ModelMapBase<TModel, TId> where TModel : ModelBase<TId>
    {
        private readonly string _tableName;
        private readonly Action<EntityTypeBuilder<TModel>> _build;

        public ModelMapBase(string tableName, Action<EntityTypeBuilder<TModel>> build)
        {
            _tableName = tableName;
            _build = build;
        }

        public void Map(EntityTypeBuilder<TModel> builder)
        {
            builder.HasKey(m => m.Id);
            builder.ToTable(_tableName);
            _build.Invoke(builder);
        }
    }

    public abstract class RemovableModelMapBase<TModel, TId> : ModelMapBase<TModel, TId> where TModel : RemovableModelBase<TId>
    {
        private static Action<EntityTypeBuilder<TModel>> GetBuild(Action<EntityTypeBuilder<TModel>> build)
        {
            return new Action<EntityTypeBuilder<TModel>>(b => {
                b.HasQueryFilter(m => !m.IsRemoved);
                build(b);
            });
        }

        public RemovableModelMapBase(string tableName, Action<EntityTypeBuilder<TModel>> build)
            : base(tableName, GetBuild(build))
        {

        }
    }
}
