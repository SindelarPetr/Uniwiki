using Shared.Exceptions;
using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions.Base
{
    public interface IIdRepository<TIdModel, TId> : IRepository<TIdModel> where TIdModel : class, IIdModel<TId>
    {
        TIdModel FindById(Guid id, string notFoundMessage) => All.Find(id) ?? throw new RequestException(notFoundMessage);

        TIdModel TryFindById(Guid id) => All.Find(id);
    }
}
