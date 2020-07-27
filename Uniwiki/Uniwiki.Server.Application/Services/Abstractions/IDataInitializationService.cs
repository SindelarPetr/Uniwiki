using Server.Appliaction.ServerActions;
using System.Threading.Tasks;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.Services
{
    public interface IDataManipulationService
    {
        Task<AddCourseResponseDto> CreateCourse(string code, string name, StudyGroupDto studyGroup, RequestContext requestContext);
        Task InitializeFakeData();
        Task<RequestContext> RegisterUser(string email, string name, string surname, string password, bool isAdmin = false);
    }
}