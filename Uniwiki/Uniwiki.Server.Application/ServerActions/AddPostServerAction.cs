using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class AddPostServerAction : ServerActionBase<AddPostRequestDto, AddPostResponseDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly ITimeService _timeService;
        private readonly IPostFileRepository _postFileRepository;
        private readonly IPostTypeRepository _postTypeRepository;
        private readonly IPostRepository _postRepository;
        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public AddPostServerAction(IServiceProvider serviceProvider, ICourseRepository courseRepository, IProfileRepository profileRepository, ITimeService timeService, IPostFileRepository postFileRepository, IPostTypeRepository postTypeRepository, IPostRepository postRepository) : base(serviceProvider)
        {
            _courseRepository = courseRepository;
            _profileRepository = profileRepository;
            _timeService = timeService;
            _postFileRepository = postFileRepository;
            _postTypeRepository = postTypeRepository;
            _postRepository = postRepository;
        }

        protected override Task<AddPostResponseDto> ExecuteAsync(AddPostRequestDto request, RequestContext context)
        {
            // Get owner profile for the post
            var profile = _profileRepository.FindById(context.User.Id);

            // Get course for the post
            var course = _courseRepository.FindById(request.CourseId);

            // Get files for the post
            var files = request.PostFiles.Select(f => (f.Id, f.NameWithoutExtension));

            // Get domain files
            var postFiles = _postFileRepository.FindPostFilesAndUpdateNames(files, profile);

            // Add the post to the DB
            var post = _postRepository.AddPost(request.PostType, profile, request.Text, course, _timeService.Now, postFiles);

            // Create DTO
            var postDto = post.ToDto(profile);

            return Task.FromResult(new AddPostResponseDto(postDto));
        }

    }
}
