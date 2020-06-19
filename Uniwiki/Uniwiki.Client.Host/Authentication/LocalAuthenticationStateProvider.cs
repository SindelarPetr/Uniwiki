using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Uniwiki.Client.Host.Services.Abstractions;

namespace Uniwiki.Client.Host.Authentication
{
    public class LocalAuthenticationStateProvider : AuthenticationStateProvider, ILocalAuthenticationStateProvider
    {
        private AuthenticationState Anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        private AuthenticationState Authenticated => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity("basic")));

        private AuthenticationState _authenticationState;

        public Guid? Token { get; private set; }

        public LocalAuthenticationStateProvider()
        {
            // Initially set the authentication state to anonymous
            _authenticationState = Anonymous;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(_authenticationState);
        }

        public void SetAsLoggedIn(Guid token)
        {
            Token = token;
            _authenticationState = Authenticated;
            NotifyAuthenticationStateChanged(Task.FromResult(_authenticationState));
        }

        public void SetAsLoggedOut()
        {
            Token = null;
            _authenticationState = Anonymous;
            NotifyAuthenticationStateChanged(Task.FromResult(_authenticationState));
        }

    }
}
