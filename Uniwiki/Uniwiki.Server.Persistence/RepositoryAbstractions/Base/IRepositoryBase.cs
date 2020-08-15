using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions.Base
{
    public interface IRepositoryBase<TModel, TId> where TModel : ModelBase<TId> where TId : struct
    {
        string NotFoundByIdMessage { get; }

        TModel FindById(TId id, string? notFoundMessage = null);

        TModel? TryFindById(TId id);
    }
}
