using System.Threading.Tasks;
using Shared.Dtos;

namespace Uniwiki.Client.Host.Services.Abstractions
{
    public interface IFixingService
    {
        Task Fix(ErrorFix errorFix);
    }
}
