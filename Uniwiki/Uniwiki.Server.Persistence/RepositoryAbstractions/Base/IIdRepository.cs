using Shared.Exceptions;
using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions.Base
{
    public interface IIdRepository<TIdModel, TId> : IRepository<TIdModel> where TIdModel : class, IIdModel<TId>
    {
        string NotFoundByIdMessage { get; }

        TIdModel FindById(Guid id, string? notFoundMessage = null) 
            => All.Find(id) ?? throw new RequestException(notFoundMessage ?? NotFoundByIdMessage);

        TIdModel TryFindById(Guid id) => All.Find(id);
    }
}
