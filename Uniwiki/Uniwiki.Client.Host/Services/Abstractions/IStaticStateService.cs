using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Client.Host.Services.Abstractions
{
    public interface IStaticStateService
    {
        StudyGroupDto? SelectedStudyGroup { get; }
        string LoginPageEmail { get; }

        string SetLoginPageEmail(string email);
        void SetSelectedStudyGroup(StudyGroupDto? selectedStudyGroup);
    }
}