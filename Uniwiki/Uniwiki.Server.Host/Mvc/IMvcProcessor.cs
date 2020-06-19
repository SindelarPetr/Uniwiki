using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Dtos;
using Shared.RequestResponse;

namespace Uniwiki.Server.Host.Mvc
{
    public interface IMvcProcessor
    {
        Task<DataForClient<IResponse>> Process(IRequest request, InputContext inputContext);
    }
}