using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class FetchPostsServerAction : ServerActionBase<FetchPostsRequestDto, FetchPostsResponseDto>
    {
        private readonly CourseRepository _courseRepository;
        private readonly PostRepository _postRepository;
        private readonly ProfileRepository _profileRepository;
        private readonly FetchPostsService _fetchPostsService;

        public FetchPostsServerAction(IServiceProvider serviceProvider, CourseRepository courseRepository, PostRepository postRepository, ProfileRepository profileRepository, FetchPostsService fetchPostsService) : base(serviceProvider)
        {
            _courseRepository = courseRepository;
            _postRepository = postRepository;
            _profileRepository = profileRepository;
            _fetchPostsService = fetchPostsService;
        }

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;
        protected override Task<FetchPostsResponseDto> ExecuteAsync(FetchPostsRequestDto request, RequestContext context)
        {
            var (postDtos, canFetchMore) = _fetchPostsService.FetchPosts(request.UsePostTypeFilter, request.CourseId, request.PostType,
                request.LastPostCreationTime, request.PostsToFetch, context.UserId);

            // Create the response
            var response = new FetchPostsResponseDto(postDtos.ToArray(), canFetchMore);

            return Task.FromResult(response);
        }
    }
}
