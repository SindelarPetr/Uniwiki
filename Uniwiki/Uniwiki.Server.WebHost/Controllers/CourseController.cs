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
    public class CourseController : Controller
    {
        private readonly ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger)
        {
            _logger = logger;
        }

        [Route(PageRoutes.CoursePage.RouteAttribute)]
        public IActionResult CoursePage(
            [FromRoute(Name = PageRoutes.CoursePage.UniversityRouteParameter)] string universityUrl, 
            [FromRoute(Name = PageRoutes.CoursePage.FacultyRouteParameter)] string facultyUrl, 
            [FromRoute(Name = PageRoutes.CoursePage.CourseRouteParameter)] string courseUrl)
        {
            return View(new CourseViewModel("UNI SHORT NAME", "UNI LONG NAME", "FAC SHORT", "FAC LONG", "COURSE NAME", "ADDA", new[] {
                new PostItemViewModel("SOME POST TEXT", "SOME CAT", new [] { new PostFileItemViewModel("SOMEFILE.pdf", "3433 MB") }
                , new PostAuthorItemViewModel("PETR SVETR", "users/svetr-jako-petr"), DateTime.Now.AddDays(-4),
                true,
                3,
                4,
                "/Uni/huuh/huhu/huhu/post")
            }));
        }
    }
}
