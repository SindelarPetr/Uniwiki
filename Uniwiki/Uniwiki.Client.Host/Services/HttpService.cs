using System.Net.Http;
using System.Threading.Tasks;
using Uniwiki.Client.Host.Services.Abstractions;

namespace Uniwiki.Client.Host.Services
{
    internal class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<HttpResponseMessage> PostAsync(string apiServer, HttpContent data)
        {
            return _httpClient.PostAsync(apiServer, data);
        }
    }
}
