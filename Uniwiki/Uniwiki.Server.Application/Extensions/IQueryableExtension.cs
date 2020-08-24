using System.Linq;
using Microsoft.EntityFrameworkCore;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Server.Application.Extensions
{
    static class IQueryableExtension
    {
        public static IQueryable<FoundCourseDto> ToFoundCourses(this IQueryable<CourseModel> courses)
            => courses
                    .Include(c => c.StudyGroup)
                    .ThenInclude(g => g.University)
                    .Select(c => new FoundCourseDto(
                        c.Id,
                        c.Url + '/' + c.StudyGroup.Url + '/' + c.StudyGroup.University.Url,
                        $"{ c.StudyGroup.University.ShortName } - { c.StudyGroup.LongName }",
                        c.Code,
                        c.FullName,
                        c.StudyGroupId));

    }
}
