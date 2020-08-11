using Microsoft.EntityFrameworkCore;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions.Base
{
    public interface IRepository<TModel> where TModel : class
    {
        void Add(TModel model);

        protected DbSet<TModel> All { get; }
    }
}
