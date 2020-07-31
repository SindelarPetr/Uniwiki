using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Shared;
using Shared.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Uniwiki.Client.Host;
using Uniwiki.Server.Application.RequestResponse;
using Uniwiki.Server.Host.Mvc;
using Uniwiki.Server.Host.Services;

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
        private readonly IConfiguration _configuration;

        public ServiceController(ILogger<ServiceController> logger, IWebHostEnvironment webHostEnvironment, ITimeService timeService, IMvcProcessor mvcProcessor, InputContextService inputContextService, IConfiguration configuration)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _timeService = timeService;
            _mvcProcessor = mvcProcessor;
            _inputContextService = inputContextService;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<string> Get([FromQuery(Name = "AccessKey")] string accessKey)
        {
            // Check the access key
            ThrowIfWrongAccessKey(accessKey);

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
        public IActionResult GetLog([FromQuery(Name = "AccessKey")] string accessKey, [FromQuery(Name = "date")] string? date = null)
        {
            // Check the access key
            ThrowIfWrongAccessKey(accessKey);

            // Check if date was not provided
            if (date == null)
            {
                // Provide the default value
                date = _timeService.Now.ToString("yyyyMMdd");
            }

            var logFileName = $"../log-{ date }.txt";
            var logFilePath = Path.GetFullPath(logFileName);

            _logger.LogInformation("Looking for a log with the name: '{LogFilePath}'", logFilePath);
            var logFileExists = System.IO.File.Exists(logFilePath);

            // Return the log if it exists
            if (logFileExists)
            {
                try
                {
                    var stream = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    return File(stream, System.Net.Mime.MediaTypeNames.Text.Plain, logFileName);
                }
                catch (Exception ex)
                {
                    return base.Problem($"We had a problem reading the log PhysicalPath:'{ logFilePath }'. Exception: {ex.Message}{Environment.NewLine}{ex.StackTrace}");
                }
            }
            else
            {
                return base.Problem($"The log '{ logFilePath }' does not exist.");
            }
        }

        /// <returns>The file used to store logs</returns>
        [HttpGet("PeekLog")]
        public IActionResult PeekLog([FromQuery(Name = "AccessKey")] string accessKey)
        {
            // Check the access key
            ThrowIfWrongAccessKey(accessKey);

            // Provide the default value
            var date = _timeService.Now.ToString("yyyyMMdd");

            var logFileName = $"../log-{ date }.txt";
            var logFilePath = Path.GetFullPath(logFileName);

            _logger.LogInformation("Looking for a log with the name: '{LogFilePath}'", logFilePath);
            var logFileExists = System.IO.File.Exists(logFilePath);

            // Return the log if it exists
            if (logFileExists)
            {
                try
                {
                    var lines = 1000;
                    var header = $"The last {lines} (or less) lines of the log named: '{ logFileName }'.{Environment.NewLine}";
                    var lastLines = ReadLastLines(lines, logFilePath);

                    return Ok(header + lastLines);
                }
                catch (Exception ex)
                {
                    return base.Problem($"We had a problem reading the log PhysicalPath:'{ logFilePath }'. Exception: {ex.Message}{Environment.NewLine}{ex.StackTrace}");
                }
            }
            else
            {
                return base.Problem($"The log '{ logFilePath }' does not exist.");
            }
        }

        private string ReadLastLines(int lastLinesCount, string fileName)
        {
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var file = new StreamReader(stream);

            var buffor = new Queue<string>(lastLinesCount);
            string? line;
            while ((line = file.ReadLine()) != null)
            {
                if (buffor.Count >= lastLinesCount)
                    buffor.Dequeue();

                buffor.Enqueue(line);
            }

            string[] lastLines = buffor.ToArray();

            string contentOfLastLines = String.Join(Environment.NewLine, lastLines);

            return contentOfLastLines;
        }

        [HttpGet("Feedback")]
        public async Task<string> GetFeedback([FromQuery(Name = "AccessKey")] string accessKey)
        {
            // Check the access key
            ThrowIfWrongAccessKey(accessKey);

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

        public void ThrowIfWrongAccessKey(string accessKey)
        {
            // Get the expected key
            var expectedAccessKey = _configuration.GetValue<string>("Uniwiki:Administration:AccessKey");

            // Check validity of the key
            if (accessKey != expectedAccessKey)
            {
                _logger.LogWarning($"Someone tried to access aministration with a wrong key! - the used key was '{accessKey}'");
                
                // Throw if key is not valid
                throw new UnauthorizedAccessException();
            }
        }
    }
}