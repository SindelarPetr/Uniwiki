using System;
using System.Collections.Generic;
using Shared.Extensions;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;

namespace Uniwiki.Server.Application.Services
{
    public class PostCategoryService : IPostCategoryService
    {
        private static readonly Dictionary<Guid, (string Category, int Count)[]> MemoryForFilters = new Dictionary<Guid, (string Category, int Count)[]>();
        private static readonly Dictionary<Guid, string[]> MemoryForNewPost = new Dictionary<Guid, string[]>();
        private readonly IPostRepository _postRepository;

        public PostCategoryService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public IEnumerable<(string Category, int Count)> GetFilterCategories(CourseModel course)
            => MemoryForFilters.GetOrDefault(course.Id, new (string Category, int Count)[0]);

        public IEnumerable<string> GetCategoriesForNewPost(CourseModel course)
            => MemoryForNewPost.GetOrDefault(course.Id, new string[0]);

        // TODO: Build up the memory from the DB - at startup
        public void UpdateMemory(CourseModel course)
        {
            MemoryForFilters[course.Id] = _postRepository.GetFilterCategories(course);
            MemoryForNewPost[course.Id] = _postRepository.GetNewPostCategories(course);
        }

        public void InitializeMemory()
        {

        }
    }
}
