using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.Repositories.Base
{

    abstract public class RepositoryBase<TModel, TId> //: IRepositoryBase<TModel, TId> 
        where TModel : ModelBase<TId> 
        where TId : struct
    {
        protected readonly UniwikiContext UniwikiContext;

        protected DbSet<TModel> All { get; }

        abstract public string NotFoundByIdMessage { get; }

        public IQueryable<TModel> FindById(TId id) => All.Where(m => m.Id.Equals(id)); //?? throw new RequestException(notFoundMessage ?? NotFoundByIdMessage);

        public RepositoryBase(UniwikiContext uniwikiContext, DbSet<TModel> all)
        {
            UniwikiContext = uniwikiContext;
            All = all;
        }

        protected void SaveChanges() => UniwikiContext.SaveChanges();
    }
}
