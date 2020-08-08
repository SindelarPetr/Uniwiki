using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class FetchPostsServerAction : ServerActionBase<FetchPostsRequestDto, FetchPostsResponseDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IPostRepository _postRepository;
        private readonly IPostTypeRepository _postTypeRepository;
        private readonly IProfileRepository _profileRepository;

        public FetchPostsServerAction(IServiceProvider serviceProvider, ICourseRepository courseRepository, IPostRepository postRepository, IPostTypeRepository postTypeRepository, IProfileRepository profileRepository) : base(serviceProvider)
        {
            _courseRepository = courseRepository;
            _postRepository = postRepository;
            _postTypeRepository = postTypeRepository;
            _profileRepository = profileRepository;
        }

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;
        protected override Task<FetchPostsResponseDto> ExecuteAsync(FetchPostsRequestDto request, RequestContext context)
        {
            // Get profile
            var profile = context.User != null ? _profileRepository.FindById(context.User.Id) : null;

            // Find the course
            var course = _courseRepository.FindById(request.CourseId);

            // Find the last post
            var lastPost = _postRepository.FindById(request.LastPostId);

            // Get posts for the 
            var posts = !request.UsePostTypeFilter
                ? _postRepository.FetchPosts(course, lastPost, request.PostsToFetch).ToArray()
                : _postRepository.FetchPosts(course, request.PostType, lastPost, request.PostsToFetch).ToArray();

            // Check if can fetch more posts
            var canFetchMore = !request.UsePostTypeFilter
                ? _postRepository.CanFetchMore(course, posts.LastOrDefault())
                : _postRepository.CanFetchMore(course, request.PostType, posts.LastOrDefault());

            // Convert posts to DTOs
            var postDtos = posts.Select(p => p.ToDto(profile)).ToArray();

            // Create the response
            var response = new FetchPostsResponseDto(postDtos, canFetchMore);

            return Task.FromResult(response);
        }
    }
}
