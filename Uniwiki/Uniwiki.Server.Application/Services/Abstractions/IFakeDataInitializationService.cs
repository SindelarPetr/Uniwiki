using System.Threading.Tasks;

namespace Uniwiki.Server.Application.Services
{
    public interface IFakeDataInitializationService
    {
        Task InitializeData();
    }
}