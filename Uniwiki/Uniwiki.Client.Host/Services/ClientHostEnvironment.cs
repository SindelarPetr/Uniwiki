using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uniwiki.Client.Host.Services
{
    public class ClientHostEnvironment : IApplicationHostEnvironment
    {
        private readonly IWebAssemblyHostEnvironment _webAssemblyHostEnvironment;

        public ClientHostEnvironment(IWebAssemblyHostEnvironment webAssemblyHostEnvironment)
        {
            _webAssemblyHostEnvironment = webAssemblyHostEnvironment;
        }

        public string Environment => _webAssemblyHostEnvironment.Environment;

        public bool IsDevelopment() => _webAssemblyHostEnvironment.IsDevelopment();

        public bool IsProduction() => _webAssemblyHostEnvironment.IsProduction();

        public bool IsStaging() => _webAssemblyHostEnvironment.IsStaging();
    }
}
