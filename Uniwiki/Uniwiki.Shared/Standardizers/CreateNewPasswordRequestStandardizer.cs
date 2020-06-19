using Shared.Standardizers;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Shared.Standardizers
{
    internal class CreateNewPasswordRequestStandardizer : IStandardizer<CreateNewPasswordRequestDto>
    {
        public CreateNewPasswordRequestDto Standardize(CreateNewPasswordRequestDto model)
        {
            return new CreateNewPasswordRequestDto(model.NewPassword, model.Secret, model.NewPasswordAgain);
        }
    }
}