using System;

namespace Uniwiki.Server.Persistence.Models.Base
{
    public abstract class ModelBase<TId> where TId : struct
    {
        public TId Id { get; set; }

        public ModelBase(TId id)
        {
            Id = id;
        }

        protected ModelBase()
        {

        }

        public override bool Equals(object obj)
        {
            if(obj.GetType() == GetType() && obj is ModelBase<TId> modelBase)
            {
                return modelBase.Id.Equals(Id);
            }

            return  base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
