using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using System;
using System.Threading.Tasks;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class AddUniversityServerAction : ServerActionBase<AddUniversityRequestDto, AddUniversityResponseDto>
    {
        private readonly UniversityRepository _universityRepository;
        private readonly TextService _textService;

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.Admin;

        public AddUniversityServerAction(IServiceProvider serviceProvider, UniversityRepository universityRepository, TextService textService) : base(serviceProvider)
        {
            _universityRepository = universityRepository;
            _textService = textService;
        }

        protected override Task<AddUniversityResponseDto> ExecuteAsync(AddUniversityRequestDto request, RequestContext context)
        {
            // Check if the name is uniq
            var nameIsUniq = _universityRepository.IsNameAndUrlUniq(request.FullName, request.Url);

            // Throw error if the name is not uniq
            if (!nameIsUniq)
            {
                throw new RequestException(_textService.Error_UniversityNameOrUrlNotUniq(request.FullName, request.Url));
            }

            // Create the university
            var university = _universityRepository.AddUniversity(request.FullName, request.ShortName, request.Url);

            // Create response
            var response = new AddUniversityResponseDto(university.ToUniversityDto());

            return Task.FromResult(response);
        }
    }
}
