using System;

namespace Uniwiki.Shared.ModelDtos
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string ProfilePictureSrc { get; set; }
        public DateTime CreationDate { get; set; }
        public string Url { get; set; }
        public string? Email { get; set; }
        public bool FeedbackProvided { get; set; }
        public string? HomeStudyGroupUrl { get; }
        public string? HomeStudyGroupLongName { get; }
        public string? HomeStudyGroupUniversityName { get; }
        public Guid? HomeStudyGroupId { get; }

        public ProfileViewModel(Guid id, string firstName, string familyName, string fullName, string profilePictureSrc, DateTime creationDate, string url, bool feedbackProvided, string? homeStudyGroupUrl, string? homeStudyGroupLongName, Guid? homeStudyGroupId, string? email, string? homeStudyGroupUniversityName)
        {
            Id = id;
            FirstName = firstName;
            FamilyName = familyName;
            FullName = fullName;
            ProfilePictureSrc = profilePictureSrc;
            CreationDate = creationDate;
            Url = url;
            Email = email;
            FeedbackProvided = feedbackProvided;
            HomeStudyGroupUrl = homeStudyGroupUrl;
            HomeStudyGroupLongName = homeStudyGroupLongName;
            HomeStudyGroupId = homeStudyGroupId;
            HomeStudyGroupUniversityName = homeStudyGroupUniversityName;
        }
    }
}