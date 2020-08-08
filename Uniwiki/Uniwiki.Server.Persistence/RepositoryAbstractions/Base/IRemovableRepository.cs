using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions.Base
{
    public interface IRemovableRepository<TRemovableModel> where TRemovableModel : class, IRemovableModel
    {
        public bool IsRemoved(TRemovableModel removableModel) => removableModel.IsRemoved;
        public void Remove(TRemovableModel removableModel) => removableModel.Remove();
        public void RevertRemove(TRemovableModel removableModel) => removableModel.RevertRemove();
    }
}
