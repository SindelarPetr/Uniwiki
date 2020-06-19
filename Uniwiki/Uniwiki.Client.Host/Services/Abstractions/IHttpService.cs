using System.Net.Http;
using System.Threading.Tasks;

namespace Uniwiki.Client.Host.Services.Abstractions
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> PostAsync(string apiServer, HttpContent data);
    }
}