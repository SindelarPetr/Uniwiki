using System.Threading.Tasks;

namespace Uniwiki.Client.Host.Services.Abstractions
{
    public interface INavigationService
    {
        void NavigateTo(string url, bool forceLoad = false);
        Task Back();
        void NavigateToTheCurrentUrl();
        void ForceReload();
        string CurrentUrl { get; }
         //TODO: GetQueryParameters();
    }
}