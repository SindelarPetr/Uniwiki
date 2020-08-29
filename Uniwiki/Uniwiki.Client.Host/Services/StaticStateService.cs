using Uniwiki.Client.Host.Services.Abstractions;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Client.Host.Services
{
    public class StaticStateService
    {
        public StudyGroupToSelectDto? SelectedStudyGroup { get; private set; }
        public void SetSelectedStudyGroup(StudyGroupToSelectDto? selectedStudyGroup) => SelectedStudyGroup = selectedStudyGroup;

        public string LoginPageEmail { get; private set; } = string.Empty;
        public string SetLoginPageEmail(string email) => LoginPageEmail = email;
    }
}
