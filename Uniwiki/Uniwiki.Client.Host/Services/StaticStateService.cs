using Uniwiki.Client.Host.Services.Abstractions;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Client.Host.Services
{
    public class StaticStateService : IStaticStateService
    {
        public StudyGroupDto? SelectedStudyGroup { get; private set; }
        public void SetSelectedStudyGroup(StudyGroupDto? selectedStudyGroup) => SelectedStudyGroup = selectedStudyGroup;

        public string LoginPageEmail { get; private set; } = string.Empty;
        public string SetLoginPageEmail(string email) => LoginPageEmail = email;
    }
}
