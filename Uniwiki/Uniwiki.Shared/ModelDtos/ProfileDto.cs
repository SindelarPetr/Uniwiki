using System;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Shared.ModelDtos
{
    public class ProfileViewModel
    {
        public Guid Id { get; }
        public string FullName { get; }
        public string FirstName { get; }
        public string FamilyName { get; }
        public string ProfilePictureSrc { get; }
        public DateTime CreationDate { get; }
        public string Url { get; }
        public bool FeedbackProvided { get;  }
        public HomeStudyGroupDto? HomeStudyGroup { get; }

        public ProfileViewModel(
            Guid id, 
            string firstName, 
            string familyName, 
            string fullName, 
            string profilePictureSrc, 
            DateTime creationDate, 
            string url, 
            bool feedbackProvided, 
            HomeStudyGroupDto? homeStudyGroup)
        {
            Id = id;
            FirstName = firstName;
            FamilyName = familyName;
            FullName = fullName;
            ProfilePictureSrc = profilePictureSrc;
            CreationDate = creationDate;
            Url = url;
            HomeStudyGroup = homeStudyGroup;
            FeedbackProvided = feedbackProvided;
        }
    }
}