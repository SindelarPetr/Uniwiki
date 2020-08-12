﻿using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions.Base
{
    public interface IRemovableRepositoryBase<TRemovableModel, TId> : IRepositoryBase<TRemovableModel, TId> where TRemovableModel : RemovableModelBase<TId>
    {
        void Remove(TRemovableModel removableModel);
        void RevertRemove(TRemovableModel removableModel);
    }
}
