using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uniwiki.Server.Persistence.Models.Base;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Shared;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Server.Persistence.Models
{
    public class ProfileModel : ModelBase<Guid>
    {
        public class Map : IEntityTypeConfiguration<ProfileModel>
        {
            public void Configure(EntityTypeBuilder<ProfileModel> builder)
            {
                builder
                    .HasMany(m => m.CourseVisits)
                    .WithOne(v => v.Profile)
                    .OnDelete(DeleteBehavior.Cascade);

                builder
                    .HasMany(m => m.PostCommentLikes)
                    .WithOne(p => p.Profile)
                    .OnDelete(DeleteBehavior.NoAction);

                builder
                    .HasMany(m => m.PostComments)
                    .WithOne(c => c.Author)
                    .OnDelete(DeleteBehavior.NoAction);

                builder
                    .HasMany(m => m.PostLikes)
                    .WithOne(p => p.Profile)
                    .OnDelete(DeleteBehavior.NoAction);

            }


        }

        [MaxLength(Constants.Validations.EmailMaxLength)]
        public string Email { get; set; } = null!;
        [MaxLength(Constants.Validations.PasswordMaxLength)]
        public string Password { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public bool IsConfirmed { get; set; }
        public AuthenticationLevel AuthenticationLevel { get; set; }
        public Guid? HomeFacultyId { get; set; }
        public StudyGroupModel? HomeFaculty { get; set; }

        public string FullName => FirstName + " " + FamilyName;
        [MaxLength(Constants.Validations.UserNameMaxLength)]
        public string FirstName { get; set; } = null!;
        [MaxLength(Constants.Validations.UserSurnameMaxLength)]
        public string FamilyName { get; set; } = null!;
        [MaxLength(Constants.Validations.UrlMaxLength)]
        public string Url { get; set; } = null!;
        [MaxLength(Constants.Validations.UrlMaxLength)]
        public string ProfilePictureSrc { get; set; } = null!;
        public ICollection<CourseVisitModel> CourseVisits { get; set; } = null!;
        public ICollection<FeedbackModel> Feedbacks { get; set; } = null!;
        public ICollection<CourseModel> CreatedCourses { get; set; } = null!;
        public ICollection<PostModel> Posts { get; set; } = null!;
        public ICollection<PostLikeModel> PostLikes { get; set; } = null!;
        public ICollection<PostCommentModel> PostComments { get; set; } = null!;
        public ICollection<PostCommentLikeModel> PostCommentLikes { get; set; } = null!;
        public ICollection<EmailConfirmationSecretModel> EmailConfirmationSecrets { get; set; } = null!;
        public ICollection<NewPasswordSecretModel> NewPasswordSecrets { get; set; } = null!;

        public ProfileModel(Guid id, string email, string firstName, string familyName, string url, string password, byte[] passwordSalt, string profilePictureSrc, DateTime creationDate, bool isConfirmed, AuthenticationLevel authenticationLevel, Guid? homeFacultyId)
            : base(id)
        {
            Email = email;
            Password = password;
            PasswordSalt = passwordSalt;
            CreationDate = creationDate;
            IsConfirmed = isConfirmed;
            AuthenticationLevel = authenticationLevel;
            HomeFacultyId = homeFacultyId;
            ProfilePictureSrc = profilePictureSrc;
            FirstName = firstName;
            FamilyName = familyName;
            Url = url;
            CourseVisits = new List<CourseVisitModel>();
            Feedbacks = new List<FeedbackModel>();
            CreatedCourses = new List<CourseModel>();
            Posts = new List<PostModel>();
            PostLikes = new List<PostLikeModel>();
            PostComments = new List<PostCommentModel>();
            PostCommentLikes = new List<PostCommentLikeModel>();
            EmailConfirmationSecrets = new List<EmailConfirmationSecretModel>();
            NewPasswordSecrets = new List<NewPasswordSecretModel>();
            EmailConfirmationSecrets = new List<EmailConfirmationSecretModel>();
            NewPasswordSecrets = new List<NewPasswordSecretModel>();
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

        internal void SetHomeFaculty(Guid? homeFacultyId) => HomeFacultyId = homeFacultyId;
    }
}
