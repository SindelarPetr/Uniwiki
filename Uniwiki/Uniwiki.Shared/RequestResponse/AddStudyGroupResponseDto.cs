using System;
using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class AddStudyGroupResponseDto : ResponseBase
    { 

        public AddStudyGroupResponseDto(Guid studyGroupId)
        {
            StudyGroupId = studyGroupId;
        }

        public Guid StudyGroupId { get; }
    }
}