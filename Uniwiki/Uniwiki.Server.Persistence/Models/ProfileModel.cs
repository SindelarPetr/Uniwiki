using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Server.Persistence.Models
{
    public class ProfileModel : IIdModel<Guid>
    {
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public byte[] PasswordSalt { get; protected set; }
        public DateTime CreationDate { get; protected set; }
        public bool IsConfirmed { get; protected set; }
        public AuthenticationLevel AuthenticationLevel { get; protected set; }
        public StudyGroupModel? HomeFaculty { get; protected set; }

        public string FullName => FirstName + " " + FamilyName;
        public string FirstName { get; protected set; }
        public string FamilyName { get; protected set; }
        public string Url { get; protected set; }
        public string ProfilePictureSrc { get; protected set; }
        public IEnumerable<CourseModel> RecentCourses { get; protected set; }
        public IEnumerable<FeedbackModel> Feedbacks { get; protected set; }
        public bool FeedbackProvided => Feedbacks.Any();
        public Guid Id { get; protected set; }

        // For Entity framework
        internal ProfileModel()
        {

        }

        internal ProfileModel(Guid id, string email, string firstName, string familyName, string url, string password, byte[] passwordSalt, string profilePictureSrc, DateTime creationDate, bool isConfirmed, AuthenticationLevel authenticationLevel, StudyGroupModel? homeFaculty, IEnumerable<CourseModel> recentCourses, IEnumerable<FeedbackModel>  feedbacks)
        {
            Id = id;
            Email = email;
            Password = password;
            PasswordSalt = passwordSalt;
            CreationDate = creationDate;
            IsConfirmed = isConfirmed;
            AuthenticationLevel = authenticationLevel;
            HomeFaculty = homeFaculty;
            RecentCourses = recentCourses;
            ProfilePictureSrc = profilePictureSrc;
            FirstName = firstName;
            FamilyName = familyName;
            Url = url;
            Feedbacks = feedbacks;
        }

        internal void SetAsConfirmed()
        {
            IsConfirmed = true;
        }

        internal void ChangePassword(string newPassword, byte[] passwordSalt)
        {
            Password = newPassword;
            PasswordSalt = passwordSalt;
        }

        internal void SetAuthenticationLevel(AuthenticationLevel authenticationLevel)
        {
            AuthenticationLevel = authenticationLevel;
        }

        internal void SetHomeFaculty(StudyGroupModel? homeFaculty)
        {
            HomeFaculty = homeFaculty;
        }
    }
}
