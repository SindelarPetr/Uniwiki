using System.Collections.Generic;
using System.Linq;
using Shared;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class PostTypeRepository : IPostTypeRepository
    {
        private readonly UniwikiContext _uniwikiContext;

        public PostTypeRepository(UniwikiContext uniwikiContext)
        {
            _uniwikiContext = uniwikiContext;
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
            var postTypes = _uniwikiContext.Posts
                .Select(p => p.PostType)
                .Concat(course.StudyGroup.PrimaryLanguage == Language.Czech ? _uniwikiContext.DefaultPostTypesCz : _uniwikiContext.DefaultPostTypesEn)
                .Distinct()
                .Where(pt => pt != null)
                .OrderBy(t => t);

            return postTypes;
        }
    }
}