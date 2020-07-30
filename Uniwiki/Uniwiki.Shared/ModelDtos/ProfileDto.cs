using System;

namespace Uniwiki.Shared.ModelDtos
{
    public class ProfileDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string ProfilePictureSrc { get; set; }
        public DateTime CreationDate { get; set; }
        public string NameIdentifier { get; set; }
        public string Email { get; set; }
        public bool FeedbackProvided { get; set; }
        public StudyGroupDto? HomeFaculty { get; }

        public ProfileDto(Guid id, string firstName, string familyName, string fullName, string profilePictureSrc, DateTime creationDate, string nameIdentifier, string email, bool feedbackProvided, StudyGroupDto? homeFaculty)
        {
            Id = id;
            FirstName = firstName;
            FamilyName = familyName;
            FullName = fullName;
            ProfilePictureSrc = profilePictureSrc;
            CreationDate = creationDate;
            NameIdentifier = nameIdentifier;
            Email = email;
            FeedbackProvided = feedbackProvided;
            HomeFaculty = homeFaculty;
        }
    }
}