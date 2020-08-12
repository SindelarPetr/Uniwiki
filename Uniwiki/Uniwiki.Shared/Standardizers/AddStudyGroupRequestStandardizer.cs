using Shared.Extensions;
using Shared.Services.Abstractions;
using Shared.Standardizers;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Shared.Standardizers
{
    internal class AddStudyGroupRequestStandardizer : IStandardizer<AddStudyGroupRequestDto>
    {
        private readonly IStringStandardizationService _stringStandardizationService;

        public AddStudyGroupRequestStandardizer(IStringStandardizationService stringStandardizationService)
        {
            _stringStandardizationService = stringStandardizationService;
        }

        public AddStudyGroupRequestDto Standardize(AddStudyGroupRequestDto model) =>
            new AddStudyGroupRequestDto(
                _stringStandardizationService.OptimizeWhiteSpaces(model.StudyGroupName).FirstCharToUpper(),
                _stringStandardizationService.OptimizeWhiteSpaces(model.StudyGroupShortcut),
                model.UniversityId, model.PrimaryLanguage);
    }
}
