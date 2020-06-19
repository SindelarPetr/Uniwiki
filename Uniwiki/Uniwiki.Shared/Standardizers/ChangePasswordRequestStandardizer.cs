using Shared.Standardizers;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Shared.Standardizers
{
    internal class ChangePasswordRequestStandardizer : IStandardizer<ChangePasswordRequestDto>
    {
        public ChangePasswordRequestDto Standardize(ChangePasswordRequestDto model)
        {
            return new ChangePasswordRequestDto(model.OldPassword, model.NewPassword, model.NewPasswordAgain);
        }
    }
}