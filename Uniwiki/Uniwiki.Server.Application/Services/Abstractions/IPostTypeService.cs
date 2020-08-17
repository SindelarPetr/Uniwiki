using System;
using System.Collections.Generic;
using System.Text;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Application.Services.Abstractions
{
    public interface IPostCategoryService
    {
        IEnumerable<(string Category, int Count)> GetFilterCategories(CourseModel course);
        object GetCategoriesForNewPost(CourseModel course);
        IEnumerable<string> GetCategoriesForNewPost(CourseModel course);
        void UpdateMemory(CourseModel course);
    }
}
