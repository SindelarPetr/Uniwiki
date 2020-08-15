namespace Uniwiki.Server.Persistence.Repositories.Base
{
    public abstract class RemovableModelBase<TModelId> : ModelBase<TModelId>
    {
        public bool IsRemoved { get; protected set; }

        public RemovableModelBase(bool isRemoved, TModelId id) 
            : base(id)
        {
            IsRemoved = isRemoved;
        }

        protected RemovableModelBase()
        {

        }

        internal void Remove() => IsRemoved = true;
        internal void RevertRemove() => IsRemoved = false;
    }
}
