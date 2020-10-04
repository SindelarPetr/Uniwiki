using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        private readonly UniversityRepository _universityRepository;
        private readonly UniwikiContext _uniwikiContext;
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        public GetUniversitiesAndStudyGroupsServerAction(IServiceProvider serviceProvider, UniversityRepository universityRepository, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _universityRepository = universityRepository;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<GetUniversitiesAndStudyGroupsResponseDto> ExecuteAsync(GetUniversitiesAndStudyGroupsRequestDto request, RequestContext context)
        {
            var universitiesWithStudyGroups = _uniwikiContext
                .StudyGroups
                .Include(g => g.University)
                .AsEnumerable()
                .GroupBy(g => g.University)
                .Select(
                    pair => new UniversityToSelectDto( 
                        pair.Key.ShortName, 
                        pair.Key.LongName, 
                        pair.Select(g => new StudyGroupToSelectDto(
                            g.ShortName, 
                            g.LongName, 
                            g.Id,
                            g.University.ShortName,
                            g.UniversityId)).ToArray() // TODO: Check the performance on ToArray on this place
                        )
                ).ToArray();
            
            var response = new GetUniversitiesAndStudyGroupsResponseDto(universitiesWithStudyGroups);

            return Task.FromResult(response);
        }
    }
}
