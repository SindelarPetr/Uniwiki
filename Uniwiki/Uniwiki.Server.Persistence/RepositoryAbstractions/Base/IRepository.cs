using Microsoft.EntityFrameworkCore;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions.Base
{
    public interface IRepository<TModel> where TModel : class
    {
        protected DbSet<TModel> All { get; }
    }
}
