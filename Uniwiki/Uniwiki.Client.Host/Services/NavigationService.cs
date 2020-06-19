using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Uniwiki.Client.Host.Services.Abstractions;

namespace Uniwiki.Client.Host.Services
{
    internal class NavigationService : INavigationService
    {
        private readonly NavigationManager _navigationManager;
        private readonly IJsInteropService _jsInteropService;

        public NavigationService(NavigationManager navigationManager, IJsInteropService jsInteropService)
        {
            _navigationManager = navigationManager;
            _jsInteropService = jsInteropService;
        }

        public void NavigateTo(string url, bool forceLoad = false)
        {
            _navigationManager.NavigateTo(url, forceLoad);
        }

        public async Task Back()
        {
            // Try navigation back
            var success = await _jsInteropService.NavigationBack();

            // If he could not navigate back
            if (!success)
                NavigateTo(_navigationManager.BaseUri);
        }

        public void NavigateToTheCurrentUrl()
        {
            _navigationManager.NavigateTo(_navigationManager.Uri);
        }

        public void ForceReload()
        {
            _navigationManager.NavigateTo(_navigationManager.Uri, true);
        }

        public string CurrentUrl => _navigationManager.Uri;
    }
}
