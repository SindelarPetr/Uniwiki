using System.Collections.Generic;
using System.Linq;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.InMemory.Services;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;

namespace Uniwiki.Server.Persistence.InMemory.Repositories
{
    internal class PostTypeInMemoryRepository : IPostTypeRepository
    {
        private readonly DataService _dataService;

        public PostTypeInMemoryRepository(DataService dataService)
        {
            _dataService = dataService;
        }

        public IEnumerable<(string?, int)> GetFilterPostTypes(CourseModel course)
        {
            return course.Posts.Select(p => p.PostType)
                .Distinct()
                .Select(pt => 
                    (PostType: pt, PostsCount: course.Posts.Count(p => p.PostType == pt)));
        }

        public IEnumerable<string> GetPostTypesForNewPost(CourseModel course)
        {
            var postTypes = course.PostTypes.Where(pt => pt != null).OrderBy(t => t);

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return postTypes;
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
    }
}