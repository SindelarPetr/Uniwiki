using Microsoft.AspNetCore.Mvc;
using Uniwiki.Client.Host;

namespace Uniwiki.Server.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return $"Server is running... \nExpected client version: ({ ClientConstants.AppVersionString })";
        }
    }
}