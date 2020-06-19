using System;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Authentication;

namespace Uniwiki.Server.Application.Services.Abstractions
{
    public interface IAuthenticationService
    {
        (LoginTokenModel?, AuthenticationLevel) TryAuthenticate(Guid? tokenId);
    }
}
