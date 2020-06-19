using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace Uniwiki.Client.Host.Services.Abstractions
{
    public interface ILocalAuthenticationStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
        void SetAsLoggedIn(Guid token);
        void SetAsLoggedOut();
        Guid? Token { get; }
    }
}
