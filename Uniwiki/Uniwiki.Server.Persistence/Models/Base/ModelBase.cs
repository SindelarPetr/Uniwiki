namespace Uniwiki.Server.Persistence.Repositories.Base
{
    public abstract class ModelBase<TId>
    {
        public TId Id { get; protected set; }

        public ModelBase(TId id)
        {
            Id = id;
        }

        protected ModelBase()
        {

        }
    }
}
