using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
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
        private readonly UniwikiContext _uniwikiContext;

        public ChangePasswordServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, TextService textService, IHashService hashService, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _textService = textService;
            _hashService = hashService;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<ChangePasswordResponseDto> ExecuteAsync(ChangePasswordRequestDto request, RequestContext context)
        {
            // Get the user
            var profile = _uniwikiContext.Profiles.Single(p => p.Id == context.UserId!.Value);

            // Hash the password
            var oldPassword = _hashService.HashPassword(request.OldPassword, profile.PasswordSalt);

            // Check if old passwords match
            if (profile.Password != oldPassword)
            {
                throw new RequestException(_textService.Error_OldPasswordsDontMatch);
            }

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