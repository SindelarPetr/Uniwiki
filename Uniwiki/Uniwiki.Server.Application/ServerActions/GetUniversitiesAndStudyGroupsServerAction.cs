using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class GetUniversitiesAndStudyGroupsServerAction : ServerActionBase<GetUniversitiesAndStudyGroupsRequestDto, GetUniversitiesAndStudyGroupsResponseDto>
    {
        private readonly IUniversityRepository _universityRepository;
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        public GetUniversitiesAndStudyGroupsServerAction(IServiceProvider serviceProvider, IUniversityRepository universityRepository) : base(serviceProvider)
        {
            _universityRepository = universityRepository;
        }

        protected override Task<GetUniversitiesAndStudyGroupsResponseDto> ExecuteAsync(GetUniversitiesAndStudyGroupsRequestDto request, RequestContext context)
        {
            var universities = _universityRepository.GetUniversities();

            var universitiesWithStudyGroups = universities.Select(
                u => new UniversityWithStudyGroupsDto(u.ToDto(), 
                    u.StudyGroups.Select(g => g.ToDto()).ToArray())
                ).ToArray();
            
            var response = new GetUniversitiesAndStudyGroupsResponseDto(universitiesWithStudyGroups);

            return Task.FromResult(response);
        }
    }
}
