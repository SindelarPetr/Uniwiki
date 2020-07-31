using Shared.RequestResponse;
using System;

namespace Uniwiki.Shared.RequestResponse
{
    public class EditHomeFacultyRequestDto : RequestBase<EditHomeFacultyResponseDto>
    {
        public EditHomeFacultyRequestDto(Guid? facultyId)
        {
            FacultyId = facultyId;
        }

        public Guid? FacultyId { get; }
    }
}
