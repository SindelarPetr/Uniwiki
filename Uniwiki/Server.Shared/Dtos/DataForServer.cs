using System;

namespace Shared.Dtos
{
    public class DataForServer
    {
        public string Type { get; set; }
        public string Request { get; set; }
        public Guid? AccessToken { get; set; }
        public Language Language { get; set; }
        public string Version { get; set; }

        // For Serialization
        public DataForServer()
        {

        }

        public DataForServer(string request, Type type, Guid? accessToken, Language language, string version) 
            : this(request, type.AssemblyQualifiedName, accessToken, language, version) { }

        public DataForServer(string request, string type, Guid? accessToken, Language language, string version)
        : this()
        {
            Type = type;
            Request = request;
            AccessToken = accessToken;
            Language = language;
            Version = version;
        }
    }
}