using System;
using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class AuthorizedUserDto
    {
        public AuthorizedUserDto(Guid id, string firstName, string familyName, string fullName, string profilePictureSrc, string url, bool feedbackProvided, string? homeStudyGroupUrl, string? homeStudyGroupLongName, Guid? homeStudyGroupId, string? email, string? homeStudyGroupShortName, string homeStudyGroupUniversityShortName)
        {
            Id = id;
            FirstName = firstName;
            FamilyName = familyName;
            FullName = fullName;
            ProfilePictureSrc = profilePictureSrc;
            Url = url;
            FeedbackProvided = feedbackProvided;
            HomeStudyGroupUrl = homeStudyGroupUrl;
            HomeStudyGroupLongName = homeStudyGroupLongName;
            HomeStudyGroupId = homeStudyGroupId;
            Email = email;
            HomeStudyGroupShortName = homeStudyGroupShortName;
            HomeStudyGroupUniversityShortName = homeStudyGroupUniversityShortName;
        }

        public Guid Id { get; }
        public string FirstName { get; }
        public string FamilyName { get; }
        public string FullName { get; }
        public string ProfilePictureSrc { get; }
        public string Url { get; }
        public bool FeedbackProvided { get; }
        public string? HomeStudyGroupUrl { get; }
        public string? HomeStudyGroupLongName { get; }
        public Guid? HomeStudyGroupId { get; }
        public string? Email { get; }
        public string? HomeStudyGroupShortName { get; }
        public string HomeStudyGroupUniversityShortName { get; }
    }

    public class ConfirmEmailResponseDto : ResponseBase
    {
        public AuthorizedUserDto AuthorizedUser { get; }
        public LoginTokenDto? LoginToken { get; }

        public ConfirmEmailResponseDto(AuthorizedUserDto authorizedUser, LoginTokenDto? loginToken)
        {
            AuthorizedUser = authorizedUser;
            LoginToken = loginToken;
        }
    }
}