using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.ServerActions.Authentication
{
    internal class RegisterServerAction : ServerActionBase<RegisterRequestDto, RegisterResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly ProfileRepository _profileRepository;
        private readonly ITimeService _timeService;
        private readonly TextService _textService;
        private readonly IStringStandardizationService _stringStandardizationService;
        private readonly IHashService _hashService;
        private readonly IEmailConfirmationSenderService _emailConfirmationSenderService;
        private readonly StudyGroupRepository _studyGroupRepository;
        private readonly IRecentCoursesService _recentCoursesService;

        public RegisterServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, ITimeService timeService, TextService textService, IStringStandardizationService stringStandardizationService, IHashService hashService, IEmailConfirmationSenderService emailConfirmationSenderService, StudyGroupRepository studyGroupRepository, IRecentCoursesService recentCoursesService)
            : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _timeService = timeService;
            _textService = textService;
            _stringStandardizationService = stringStandardizationService;
            _hashService = hashService;
            _emailConfirmationSenderService = emailConfirmationSenderService;
            _studyGroupRepository = studyGroupRepository;
            _recentCoursesService = recentCoursesService;
        }

        protected override async Task<RegisterResponseDto> ExecuteAsync(RegisterRequestDto request, RequestContext context)
        {
            // Try to get profile
            var profile = _profileRepository.TryGetProfileByEmail(request.Email);

            // Email is already registered and confirmed
            if (profile != null && profile.IsConfirmed)
            {
                throw new RequestException(_textService.Error_EmailIsAlreadyUsed(request.Email));
            }
            
            // Register user if he is not registered yet
            if(profile == null)
            {
                // Get the name and surname
                var names = request.NameAndSurname.Split( new[] { ' ' }, 2);

                // Create url for the new profile
                string url = _stringStandardizationService.CreateUrl(request.NameAndSurname,
                    u => _profileRepository.TryGetProfileByUrl(u) == null);

                // Get the hash from the password
                var password = _hashService.HashPassword(request.Password);

                // Get the home faculty
                var homeFaculty = request.HomeFacultyId.HasValue ? _studyGroupRepository.FindById(request.HomeFacultyId.Value) : null;

                // Create the profile
                profile = _profileRepository.AddProfile(request.Email, names[0], names[1], url, password.hashedPassword, password.salt, $"/img/profilePictures/no-profile-picture.jpg", _timeService.Now, false, AuthenticationLevel.PrimaryToken, homeFaculty);
            }

            // Set the recent courses
            _recentCoursesService.SetAsRecentCourses(request.RecentCourses, profile.Id);

            // Send the confirmation email
            await _emailConfirmationSenderService.SendConfirmationEmail(profile);

            return new RegisterResponseDto(profile.ToDto());
        }
    }
}
