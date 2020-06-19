using Shared.RequestResponse;

namespace Server.Appliaction.ServerActions
{
    public interface IServerActionResolver
    {
        IServerAction Resolve(IRequest request);
    }
}