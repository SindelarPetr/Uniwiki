using Microsoft.EntityFrameworkCore;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.Repositories.Base
{
    internal abstract class RepositoryBase<TModel> : IRepository<TModel> where TModel : class
    {
        protected DbSet<TModel> All { get; }

        DbSet<TModel> IRepository<TModel>.All => All;

        private readonly UniwikiContext _uniwikiContext;

        public RepositoryBase(UniwikiContext uniwikiContext, DbSet<TModel> all)
        {
            _uniwikiContext = uniwikiContext;
            All = all;
        }

        protected void SaveChanges() => _uniwikiContext.SaveChanges();

        public void Add(TModel model)
        {
            All.Add(model);

            SaveChanges();
        }
    }
}
