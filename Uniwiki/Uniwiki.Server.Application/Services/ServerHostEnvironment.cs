using Microsoft.AspNetCore.Hosting;
using Shared.Services.Abstractions;

namespace Uniwiki.Server.Application.Services
{
    public class ServerHostEnvironment : IApplicationHostEnvironment
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ServerHostEnvironment(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string Environment => _hostingEnvironment.EnvironmentName;

        public bool IsDevelopment() => _hostingEnvironment.IsDevelopment();

        public bool IsProduction() => _hostingEnvironment.IsProduction();

        public bool IsStaging() => _hostingEnvironment.IsStaging();
    }
}
