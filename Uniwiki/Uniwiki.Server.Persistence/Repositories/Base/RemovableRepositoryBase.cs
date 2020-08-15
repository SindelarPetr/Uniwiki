using Microsoft.EntityFrameworkCore;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.Repositories.Base
{
    internal abstract class RemovableRepositoryBase<TRemovableModel, TId> : RepositoryBase<TRemovableModel, TId>, IRemovableRepositoryBase<TRemovableModel, TId> 
        where TRemovableModel : RemovableModelBase<TId>
        where TId : struct
    {
        public RemovableRepositoryBase(UniwikiContext uniwikiContext, DbSet<TRemovableModel> all)
            : base(uniwikiContext, all)
        {

        }

        public void Remove(TRemovableModel removableModel) => removableModel.Remove();
        public void RevertRemove(TRemovableModel removableModel) => removableModel.RevertRemove();
    }
}
