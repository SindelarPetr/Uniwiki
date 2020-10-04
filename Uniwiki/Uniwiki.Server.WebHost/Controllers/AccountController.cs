using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Uniwiki.Server.WebHost.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [Route(PageRoutes.AccountPage.RouteAttribute)]
        public IActionResult AccountPage()
        {
            return View();
        }

        [Route(PageRoutes.RegisterPage.RouteAttribute)]
        public IActionResult RegisterPage()
        {
            return View();
        }

        [Route(PageRoutes.LoginPage.RouteAttribute)]
        public IActionResult LogInPage()
        {
            return View();
        }
    }
}
