using System;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;

namespace Uniwiki.Server.Host.Mvc
{
    public class KestrelServerOptionsSetup : IConfigureOptions<KestrelServerOptions>
    {
        private IServiceProvider _services;

        public KestrelServerOptionsSetup(IServiceProvider services)
        {
            _services = services;
        }

        public void Configure(KestrelServerOptions options)
        {
            options.ApplicationServices = _services;
        }
    }
}


