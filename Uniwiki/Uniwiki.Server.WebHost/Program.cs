using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Uniwiki.Server.WebHost
{
    /*
        Azure App ID: a3e2ced7-e9c9-4666-a463-6f0dfe46ddf6
        Azure AD Secret: 6neb.3_c_Z8uS_7M~L3FDk_x5UBvQR5W8G
        Facebook AppId: 382140665902076
        Facebook secret: 3e736cfe6fd4a62b5f27e6f2d98639d6
     */

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
