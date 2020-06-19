using System.Threading.Tasks;
using Shared.RequestResponse;

namespace Server.Appliaction.ServerActions
{
    public interface IServerAction
    {
        Task<IResponse> ExecuteActionAsync(IRequest request, RequestContext context);
    }
}