using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.ServerActions.Authentication
{
    internal class ChangePasswordServerAction : ServerActionBase<ChangePasswordRequestDto, ChangePasswordResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.PrimaryToken;

        private readonly ProfileRepository _profileRepository;
        private readonly TextService _textService;
        private readonly IHashService _hashService;

        public ChangePasswordServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, TextService textService, IHashService hashService) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _textService = textService;
            _hashService = hashService;
        }

        protected override Task<ChangePasswordResponseDto> ExecuteAsync(ChangePasswordRequestDto request, RequestContext context)
        {
            // Get profile for the user
            var profile = context.User ?? throw new NullReferenceException();

            // Hash the password
            var oldPassword = _hashService.HashPassword(request.OldPassword, profile.PasswordSalt);

            // Check if old passwords match
            if (profile.Password != oldPassword)
                throw new RequestException(_textService.Error_OldPasswordsDontMatch);

            // Hash the password
            var newPassword = _hashService.HashPassword(request.NewPassword);

            // Change the password
            _profileRepository.ChangePassword(profile, newPassword.hashedPassword, newPassword.salt);
            
            // Create response
            var response = new ChangePasswordResponseDto();

            return Task.FromResult(response);
        }
    }
}