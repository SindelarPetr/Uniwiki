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

        public AddPostRequestDto Standardize(AddPostRequestDto model) =>
            new AddPostRequestDto(
                model.Text.FirstCharToUpper().Trim(),
                _stringStandardizationService.OptimizeWhiteSpaces(model.PostType?.Trim().FirstCharToUpper()),
                model.CourseId,
                model.PostFiles);
    }
}
