using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Uniwiki.Server.WebHost.ViewModels;

namespace Uniwiki.Server.WebHost.Controllers
{
    public class UniController : Controller
    {
        private readonly ILogger<UniController> _logger;

        public UniController(ILogger<UniController> logger)
        {
            _logger = logger;
        }

        [Route(PageRoutes.UniversityPage.RouteAttribute)]
        public IActionResult UniversityPage(
            [FromRoute(Name = PageRoutes.UniversityPage.UniversityRouteParameter)] string universityUrl)
        {
            return View(new UniversityViewModel("SHORT", universityUrl, new[] {
                new FacultyItemViewModel("FAC1", "FAC2", "/Uni/univer/facu"),
            new FacultyItemViewModel("FAC1", "FAC2", "/Uni/univer/facu"),
            new FacultyItemViewModel("FAC1", "FAC2", "/Uni/univer/facu")}));
        }
    }
}
