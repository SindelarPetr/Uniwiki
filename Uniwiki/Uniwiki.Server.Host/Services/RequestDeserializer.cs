using System;
using Newtonsoft.Json;
using Shared.RequestResponse;
using Uniwiki.Server.Host.Services.Abstractions;

namespace Uniwiki.Server.Host.Services
{
    public class RequestDeserializer : IRequestDeserializer
    {
        public IRequest Deserialize(string request, string type)
        {
            // Get the type of the received request
            var requestType = Type.GetType(type);

            // Deserialize the request
            return JsonConvert.DeserializeObject(request, requestType) as IRequest
                   ?? throw new Exception($"The request type '{requestType}' could not be deserialized.");
        }
    }
}