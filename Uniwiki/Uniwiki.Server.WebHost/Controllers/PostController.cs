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
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger)
        {
            _logger = logger;
        }

        [Route(PageRoutes.PostPage.RouteAttribute)]
        public IActionResult PostPage(
            [FromRoute(Name = PageRoutes.PostPage.UniversityRouteParameter)] string universityUrl,
            [FromRoute(Name = PageRoutes.PostPage.FacultyRouteParameter)] string facultyUrl,
            [FromRoute(Name = PageRoutes.PostPage.CourseRouteParameter)] string courseUrl,
            [FromRoute(Name = PageRoutes.PostPage.PostRouteParameter)] string postUrl)
        {
            return View(new PostViewModel(
                "UNI SHORT NAME",
                "FAC LONG NAME",
                true,
                5,
                DateTime.Now,
                new[] {
                new PostCommentViewModel("PEtr","/profile/pepep",DateTime.Now, "TEXT OF THE GOD DAMN COMMENT YEAHHHHH BRO", 4, true),
                new PostCommentViewModel("PEtr","/profile/pepep",DateTime.Now, "TEXT OF THE GOD DAMN COMMENT YEAHHHHH BRO", 4, true),
                },
                "PPEPEP",
                "/profile/pepsssep",
                "Course god damn",
                new[] {
                    new PostFileItemViewModel("SOMEFILE.pdf", "3433 MB"),
                new PostFileItemViewModel("SOMEFILE.pdf", "3433 MB")},
                "ČVUT",
                "cvut",
                "Fakulta Informačních Technologií",
                "fit"));
        }

        [Route(PageRoutes.AddPostPage.RouteAttribute)]
        public IActionResult AddPostPage(
            [FromRoute(Name = PageRoutes.AddPostPage.UniversityRouteParameter)] string universityUrl,
            [FromRoute(Name = PageRoutes.AddPostPage.FacultyRouteParameter)] string facultyUrl,
            [FromRoute(Name = PageRoutes.AddPostPage.CourseRouteParameter)] string courseUrl
        )
        {
            return View(new AddOrEditPostViewModel(
                "ČVUT",
                "cvut",
                "Fakulta Informačních Technologií",
                "fit",
                "linearni algebra",
                "linearni-algebra",
                new[] { "One", "Two", "Three" }
                ));
        }
    }
}
