using System.Runtime.Serialization;
using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    [DataContract]
    public class RegisterResponseDto : ResponseBase
    {
        [DataMember]
        public ProfileDto UserProfile { get; set; }

        public RegisterResponseDto(ProfileDto userProfile)
        {
            UserProfile = userProfile;
        }
    }
}
