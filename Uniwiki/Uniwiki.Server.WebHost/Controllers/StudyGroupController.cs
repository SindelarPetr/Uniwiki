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
    public class StudyGroupController : Controller
    {
        private readonly ILogger<StudyGroupController> _logger;

        public StudyGroupController(ILogger<StudyGroupController> logger)
        {
            _logger = logger;
        }

        [Route(PageRoutes.FacultyPage.RouteAttribute)]
        public IActionResult FacultyPage(
            [FromRoute(Name = PageRoutes.FacultyPage.UniversityRouteParameter)] string universityUrl, 
            [FromRoute(Name = PageRoutes.FacultyPage.FacultyRouteParameter)] string facultyUrl)
        {
            return View(new FacultyViewModel(universityUrl, "UNI LONG NAME AAASSDDFGGGHHHH", facultyUrl, "LONG AS ANA<EMMMEMEMEMEMEMEMME", new[] {
            new CourseItemViewModel("Course numero unoooos sjsjs1", "ffddss", "/Uni/univer/facu/cours"),
            new CourseItemViewModel("Course numero unoooos sjsjs2", "ffddss", "/Uni/univer/facu/cours"),
            new CourseItemViewModel("Course numero unoooos sjsjs3", "ffddss", "/Uni/univer/facu/cours"),
            }));
        }
    }
}
