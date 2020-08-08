namespace Uniwiki.Server.Persistence.Repositories.Base
{
    public interface IRemovableModel
    {
        public bool IsRemoved { get; internal set; }

        internal void Remove() => IsRemoved = true;
        internal void RevertRemove() => IsRemoved = false;
    }

    

    
    // -------------------------- /REPOSITORIES
}
