using Microsoft.EntityFrameworkCore;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions.Base
{
    public interface IRepository<TModel> where TModel : class
    {
        public DbSet<TModel> All { get; }

        public void Add(TModel model) => All.Add(model);
    }
}
