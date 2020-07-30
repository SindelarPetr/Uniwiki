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
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.ServerActions.Authentication
{
    internal class RegisterServerAction : ServerActionBase<RegisterRequestDto, RegisterResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly IProfileRepository _profileRepository;
        private readonly ITimeService _timeService;
        private readonly TextService _textService;
        private readonly IStringStandardizationService _stringStandardizationService;
        private readonly IHashService _hashService;
        private readonly IEmailConfirmationSenderService _emailConfirmationSenderService;
        private readonly IStudyGroupRepository _studyGroupRepository;
        private readonly IRecentCoursesService _recentCoursesService;

        public RegisterServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository, ITimeService timeService, TextService textService, IStringStandardizationService stringStandardizationService, IHashService hashService, IEmailConfirmationSenderService emailConfirmationSenderService, IStudyGroupRepository studyGroupRepository, IRecentCoursesService recentCoursesService)
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

                // Check if there is a name and surname (even though it should be already checked by a validator)
                if (names.Length != 2)
                    throw new RequestException(_textService.Validation_FillNameAndSurname);

                // Create url for the new profile
                string url = _stringStandardizationService.CreateUrl(request.NameAndSurname,
                    u => _profileRepository.TryGetProfileByUrl(u) == null);

                // Get the hash from the password
                var password = _hashService.HashPassword(request.Password);

                // Get the home faculty
                var homeFaculty = request.HomeFacultyId.HasValue ? _studyGroupRepository.FindById(request.HomeFacultyId.Value) : null;

                // Create a new profile
                profile = _profileRepository.Register(request.Email, names[0], names[1], url, password.hashedPassword, password.salt, _timeService.Now, homeFaculty);
            }

            // Set the recent courses
            _recentCoursesService.SetAsRecentCourses(request.RecentCourses, profile);

            // Send the confirmation email
            await _emailConfirmationSenderService.SendConfirmationEmail(profile);

            return new RegisterResponseDto(profile.ToDto());
        }
    }
}
