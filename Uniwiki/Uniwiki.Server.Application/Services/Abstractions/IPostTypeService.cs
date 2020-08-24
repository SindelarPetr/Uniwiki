using System.Collections.Generic;
using Shared;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Application.Services.Abstractions
{
    public interface IPostCategoryService
    {
        IEnumerable<(string Category, int Count)> GetFilterCategories(CourseModel course);
        IEnumerable<string> GetCategoriesForNewPost(CourseModel course);
        void UpdateMemory(CourseModel course, Language language);
    }
}
