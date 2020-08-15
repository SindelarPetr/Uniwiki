namespace Uniwiki.Server.Persistence.Repositories.Base
{
    public abstract class RemovableModelBase<TId> : ModelBase<TId> 
        where TId : struct
    {
        public bool IsRemoved { get; protected set; }

        public RemovableModelBase(bool isRemoved, TId id) 
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
