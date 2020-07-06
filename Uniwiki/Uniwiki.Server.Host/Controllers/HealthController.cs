using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uniwiki.Client.Host;

namespace Uniwiki.Server.Host.Controllers
{
    [ApiController]
    [Route("api/Health")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HealthController(ILogger<HealthController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public string Get()
        {
            return $"Server is running... \nExpected client version: ({ ClientConstants.AppVersionString })";
        }

        /// <returns>The file used to store logs</returns>
        [HttpGet("Log")]
        public FileResult GetLog()
        {
            var logFile = _webHostEnvironment.ContentRootFileProvider.GetFileInfo("./bin/log.json");

            return File(logFile.CreateReadStream(), System.Net.Mime.MediaTypeNames.Application.Octet, "Log.json");
        }
    }
}