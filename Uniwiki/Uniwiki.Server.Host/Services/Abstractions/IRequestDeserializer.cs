using Shared.RequestResponse;

namespace Uniwiki.Server.Host.Services.Abstractions
{
    public interface IRequestDeserializer
    {
        IRequest Deserialize(string request, string type);
    }
}