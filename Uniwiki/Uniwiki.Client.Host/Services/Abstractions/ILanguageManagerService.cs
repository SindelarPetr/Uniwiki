using System.Threading.Tasks;
using Shared;

namespace Uniwiki.Client.Host.Services.Abstractions
{
    public interface ILanguageManagerService
    {
        Language CurrentLanguage { get; }
        Task InitializeLanguage();
        Task SetLanguage(Language language);
    }
}