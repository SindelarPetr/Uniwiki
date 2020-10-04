using System;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class AuthorizedUserDto
    {
        public AuthorizedUserDto(Guid id, string firstName, string familyName, string fullName, string profilePictureSrc, string url, bool feedbackProvided, string email, HomeStudyGroupDto? homeStudyGroup)
        {
            Id = id;
            FirstName = firstName;
            FamilyName = familyName;
            FullName = fullName;
            ProfilePictureSrc = profilePictureSrc;
            Url = url;
            FeedbackProvided = feedbackProvided;
            Email = email;
            HomeStudyGroup = homeStudyGroup;
        }

        public string Email { get; set; }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string FullName { get; set; }
        public string ProfilePictureSrc { get; set; }
        public string Url { get; set; }
        public bool FeedbackProvided { get; set; }
        public HomeStudyGroupDto? HomeStudyGroup { get; set; }
        public bool HasHomeStudyGroup => HomeStudyGroup != null;
    }
}