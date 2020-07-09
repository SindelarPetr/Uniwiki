using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uniwiki.Client.Host;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Server.Host.Controllers
{
    [ApiController]
    [Route("api/Health")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ITimeService _timeService;

        public HealthController(ILogger<HealthController> logger, IWebHostEnvironment webHostEnvironment, ITimeService timeService)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _timeService = timeService;
        }

        [HttpGet]
        public string Get()
        {
            return $"Server is running... \nExpected client version: ({ ClientConstants.AppVersionString })\nServer time: {_timeService.Now}";
        }

        /// <returns>The file used to store logs</returns>
        [HttpGet("Log")]
        public IActionResult GetLog([FromQuery(Name = "date")] string? date = null)
        {
            // Check if date was not provided
            if(date == null)
            {
                // Provide the default value
                date = _timeService.Now.ToString("yyyyMMdd");
            }

            var logFileName = $"./bin/log-{ date }.json";

            _logger.LogInformation("Looking for a log with the name: '{LogFileName}'", logFileName);
            var logFile = _webHostEnvironment.ContentRootFileProvider.GetFileInfo(logFileName);

            return File(logFile.CreateReadStream(), System.Net.Mime.MediaTypeNames.Text.Plain, $"Log-{date}.json");
        }
    }
}