using Shared.Extensions;
using Shared.Services;
using Shared.Standardizers;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Shared.Standardizers
{
    public class AddPostRequestStandardizer : IStandardizer<AddPostRequestDto>
    {
        private readonly IStringStandardizationService _stringStandardizationService;

        public AddPostRequestStandardizer(IStringStandardizationService stringStandardizationService)
        {
            _stringStandardizationService = stringStandardizationService;
        }

        public AddPostRequestDto Standardize(AddPostRequestDto model)
        {

            var postCategory = string.IsNullOrWhiteSpace(model.PostType) ? null : _stringStandardizationService.OptimizeWhiteSpaces(model.PostType?.Trim().FirstCharToUpper());
            return new AddPostRequestDto(
model.Text.FirstCharToUpper().Trim(),
postCategory,
model.CourseId,
model.PostFiles);
        }
    }
}
