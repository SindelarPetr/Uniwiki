using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Server.Persistence.Models
{
    public class ProfileModel : ModelBase<Guid>
    {
        public string Email { get; protected set; } = null!;
        public string Password { get; protected set; } = null!;
        public byte[] PasswordSalt { get; protected set; } = null!;
        public DateTime CreationDate { get; protected set; }
        public bool IsConfirmed { get; protected set; }
        public AuthenticationLevel AuthenticationLevel { get; protected set; }
        public Guid? HomeFacultyId { get; protected set; }
        public StudyGroupModel? HomeFaculty { get; protected set; }

        public string FullName => FirstName + " " + FamilyName;
        public string FirstName { get; protected set; } = null!;
        public string FamilyName { get; protected set; } = null!;
        public string Url { get; protected set; } = null!;
        public string ProfilePictureSrc { get; protected set; } = null!;
        public ICollection<CourseVisitModel> CourseVisits { get; protected set; } 
            = new List<CourseVisitModel>();
        public ICollection<FeedbackModel> Feedbacks { get; set; }
            = new List<FeedbackModel>();
        public bool FeedbackProvided => Feedbacks.Any();

        internal ProfileModel(Guid id, string email, string firstName, string familyName, string url, string password, byte[] passwordSalt, string profilePictureSrc, DateTime creationDate, bool isConfirmed, AuthenticationLevel authenticationLevel, StudyGroupModel? homeFaculty)
            : base(id)
        {
            Email = email;
            Password = password;
            PasswordSalt = passwordSalt;
            CreationDate = creationDate;
            IsConfirmed = isConfirmed;
            AuthenticationLevel = authenticationLevel;
            HomeFacultyId = homeFaculty?.Id;
            HomeFaculty = homeFaculty;
            ProfilePictureSrc = profilePictureSrc;
            FirstName = firstName;
            FamilyName = familyName;
            Url = url;
        }

        // For Entity framework
        protected ProfileModel()
        {

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

        internal void SetAuthenticationLevel(AuthenticationLevel authenticationLevel) => AuthenticationLevel = authenticationLevel;

        internal void SetHomeFaculty(StudyGroupModel? homeFaculty) => HomeFaculty = homeFaculty;
    }
}
