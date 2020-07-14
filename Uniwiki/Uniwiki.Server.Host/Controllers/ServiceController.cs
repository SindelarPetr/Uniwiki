using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Uniwiki.Client.Host;
using Uniwiki.Server.Application.ServerActions;
using Uniwiki.Server.Host.Mvc;
using Uniwiki.Server.Host.Services;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Server.Host.Controllers
{
    [ApiController]
    [Route("api/Service")]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ITimeService _timeService;
        private readonly IMvcProcessor _mvcProcessor;
        private readonly InputContextService _inputContextService;

        public ServiceController(ILogger<ServiceController> logger, IWebHostEnvironment webHostEnvironment, ITimeService timeService, IMvcProcessor mvcProcessor, InputContextService inputContextService)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _timeService = timeService;
            _mvcProcessor = mvcProcessor;
            _inputContextService = inputContextService;
        }

        [HttpGet]
        public string Get()
        {
            try
            {
                DriveInfo drive = new DriveInfo(Path.GetDirectoryName(typeof(ServiceController).Assembly.Location));

                var totalBytes = drive.TotalSize;
                var freeBytes = drive.AvailableFreeSpace;

                var freePercent = freeBytes / (double)totalBytes;

                var ip = HttpContext.Connection.RemoteIpAddress;
                return
                    $"Server is running!\n" +
                    $"Expected client version: ({ ClientConstants.AppVersionString })\n" +
                    $"Server time: {_timeService.Now}\n" +
                    $"Client IP: {ip}\n" +
                    $"ASP.NET Environment: {_webHostEnvironment.EnvironmentName}\n" +
                    //$"Configuration: {}\n" +
                    $"Storage size: {drive.TotalSize / 1_000_000.0:N} MB\n" +
                     $"Storage available free space: {drive.AvailableFreeSpace / 1_000_000.0:N} MB ({freePercent:P})\n" +
                     $"Storage total free space: {drive.TotalFreeSpace / 1_000_000.0:N} MB\n";

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
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

        [HttpGet("Feedback")]
        public async Task<string> GetFeedback()
        {
            // Create input context
            var inputContext = _inputContextService.CreateFromHttpContext(null, Language.English, ClientConstants.AppVersionString, HttpContext);

            var feedbacksCount = 100;
            var request = new GetFeedbackSummaryRequestDto(feedbacksCount);

            var clientData = await _mvcProcessor.Process(request, inputContext);

            var response = clientData.Response as GetFeedbackSummaryResponseDto;

            if (response == null)
                return "Error";

            return
                $"Average rating: {response.AverageRating}\n" +
                $"Feedbacks count total: {response.FeedbackCount}\n" +
                $"Text only feedbacks count: {response.TextOnlyFeedbacksCount} ({response.TextOnlyFeedbacksCountPercentage} %)\n" +
                $"Ratings only feedbacks count: {response.RatingOnlyFeedbacksCount} ({response.RatingOnlyFeedbacksCountPercentage} %)\n" +
                $"Text and rating feedbacks count: {response.TextAndRatingFeedbacksCount} ({response.TextAndRatingFeedbacksCountPercentage} %)\n" +
                $"Last {feedbacksCount} feedbacks: \n{response.Feedbacks.Aggregate(string.Empty, (a, b) => $"{a}\n{b}")}";
        }
    }
}