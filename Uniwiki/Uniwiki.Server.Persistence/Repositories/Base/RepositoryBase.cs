using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.Repositories.Base
{

    internal abstract class RepositoryBase<TModel, TId> : IRepositoryBase<TModel, TId> where TModel : ModelBase<TId>
    {
        private readonly UniwikiContext _uniwikiContext;

        protected DbSet<TModel> All { get; }

        public abstract string NotFoundByIdMessage { get; }

        public TModel FindById(TId id, string? notFoundMessage = null) => All.Find(id) ?? throw new RequestException(notFoundMessage ?? NotFoundByIdMessage);

        public TModel? TryFindById(TId id) => All.Find(id);

        public RepositoryBase(UniwikiContext uniwikiContext, DbSet<TModel> all)
        {
            _uniwikiContext = uniwikiContext;
            All = all;
        }

        protected void SaveChanges() => _uniwikiContext.SaveChanges();
    }
}
