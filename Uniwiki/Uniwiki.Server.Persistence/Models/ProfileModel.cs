using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Models.Authentication;

namespace Uniwiki.Server.Persistence.Models
{
    public class ProfileModel
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Password { get; private set; } // Setter for changing password
        public byte[] PasswordSalt { get; private set; } // Setter for changing password
        public DateTime CreationDate { get; }
        public bool IsConfirmed { get; private set; } // Setter for confirming
        public AuthenticationLevel AuthenticationLevel { get; private set; }

        public string FullName => FirstName + " " + FamilyName;
        public string FirstName { get; }
        public string FamilyName { get; }
        public string Url { get; set; }
        public string ProfilePictureSrc { get; }
        public IEnumerable<CourseModel> RecentCourses { get; }
        
        public ProfileModel(Guid id, string email, string firstName, string familyName, string url, string password, byte[] passwordSalt, string profilePictureSrc, DateTime creationDate, bool isConfirmed, AuthenticationLevel authenticationLevel, IEnumerable<CourseModel> recentCourses) 
        {
            Id = id;
            Email = email;
            Password = password;
            PasswordSalt = passwordSalt;
            CreationDate = creationDate;
            IsConfirmed = isConfirmed;
            AuthenticationLevel = authenticationLevel;
            RecentCourses = recentCourses;
            ProfilePictureSrc = profilePictureSrc;
            FirstName = firstName;
            FamilyName = familyName;
            Url = url;
        }

        public void SetAsConfirmed()
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
    }
}
