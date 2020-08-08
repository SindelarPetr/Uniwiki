using System;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Application.Services.Abstractions
{
    public interface IAuthenticationService
    {
        (LoginTokenModel?, AuthenticationLevel) TryAuthenticate(Guid? tokenId);
    }
}
