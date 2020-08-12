using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence
{
    internal class UniwikiContext : DbContext
    {
        public DbSet<LoginTokenModel> LoginTokens => Set<LoginTokenModel>();

        public DbSet<NewPasswordSecretModel> NewPasswordSecrets => Set<NewPasswordSecretModel>();

        public DbSet<EmailConfirmationSecretModel> EmailConfirmationSecrets => Set<EmailConfirmationSecretModel>();

        public DbSet<ProfileModel> Profiles => Set<ProfileModel>();

        public DbSet<UniversityModel> Universities => Set<UniversityModel>();

        public DbSet<StudyGroupModel> StudyGroups => Set<StudyGroupModel>();

        public DbSet<CourseVisitModel> CourseVisits => Set<CourseVisitModel>();

        public DbSet<CourseModel> Courses => Set<CourseModel>();
         
        public DbSet<PostModel> Posts => Set<PostModel>();

        // TODO: Create the post file to be removable
        // TODO: Keep track of every post file, which is not used
        public DbSet<PostFileModel> PostFiles => Set<PostFileModel>();

        public DbSet<PostCommentModel> PostComments => Set<PostCommentModel>();

        public DbSet<PostLikeModel> PostLikes => Set<PostLikeModel>();

        public DbSet<PostCommentLikeModel> PostCommentLikes => Set<PostCommentLikeModel>();

        public DbSet<PostFileDownloadModel> PostFileDownloads => Set<PostFileDownloadModel>();

        public DbSet<FeedbackModel> Feedbacks => Set<FeedbackModel>();

        // TODO: Move to configuration
        public IEnumerable<string> DefaultPostTypesCz => new[]
        {
            "Domácí úkol",
            "Test v semesteru",
            "Zkouška",
            "Studijní materiál"
        };

        // TODO: Move to configuration
        public IEnumerable<string> DefaultPostTypesEn => new[]
        {
            "Homework",
            "Test in semester",
            "Final exam",
            "Study material"
        };

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);

            // TODO: Move the connection string to the configuration
            // TODO: Make it possible to switch between the options in configuration
            // options.UseSqlServer("Server=localhost\\SQLEXPRESS01; initial catalog=UniwikiLocalDatabase;Database=master;Trusted_Connection=True; integrated security=SSPI");
            options.UseInMemoryDatabase("UniwikiLocalDatabase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudyGroupModel>()
            .HasOne(a => a.Profile)
            .WithOne(a => a.HomeFaculty)
            .HasForeignKey<StudyGroupModel>(c => c.Id);

            modelBuilder.Entity<PostCommentModel>()
                .HasMany(c => c.Likes)
                .WithOne(l => l.Comment);

            modelBuilder.Entity<PostCommentLikeModel>()
                .HasKey(e => new PostCommentLikeModelId(e.CommentId, e.ProfileId));

            modelBuilder.Entity<PostModel>()
                .HasMany(p => p.Likes)
                .WithOne(l => l.Post);

            modelBuilder.Entity<PostLikeModel>()
                .HasKey(e => new PostLikeModelId(e.PostId, e.ProfileId));

            modelBuilder.Entity<PostCategoryModel>()
                .HasKey(m => new PostCategoryModelId(m.Name, m.CourseId));
        }

    }

    public struct PostCategoryModelId
    {
        public string Name;
        public Guid CourseId;

        public PostCategoryModelId(string name, CourseModel course)
            : this(name, course.Id) { }

        public PostCategoryModelId(string name, Guid courseId)
        {
            Name = name;
            CourseId = courseId;
        }

        public override bool Equals(object? obj)
        {
            return obj is PostCategoryModelId other &&
                   Name.Equals(other.Name) &&
                   CourseId.Equals(other.CourseId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, CourseId);
        }

        public void Deconstruct(out string name, out Guid courseId)
        {
            name = Name;
            courseId = CourseId;
        }

        public static implicit operator (string Name, Guid CourseId)(PostCategoryModelId value)
        {
            return (value.Name, value.CourseId);
        }

        public static implicit operator PostCategoryModelId((string Name, Guid CourseId) value)
        {
            return new PostCategoryModelId(value.Name, value.CourseId);
        }
    }

    public struct PostLikeModelId
    {
        public Guid PostId;
        public Guid ProfileId;

        public PostLikeModelId(PostModel post, ProfileModel profile)
            : this(post.Id, profile.Id) { }

        public PostLikeModelId(Guid postId, Guid profileId)
        {
            PostId = postId;
            ProfileId = profileId;
        }

        public override bool Equals(object? obj)
        {
            return obj is PostLikeModelId other &&
                   PostId.Equals(other.PostId) &&
                   ProfileId.Equals(other.ProfileId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PostId, ProfileId);
        }

        public void Deconstruct(out Guid postId, out Guid profileId)
        {
            postId = PostId;
            profileId = ProfileId;
        }

        public static implicit operator (Guid PostId, Guid ProfileId)(PostLikeModelId value)
        {
            return (value.PostId, value.ProfileId);
        }

        public static implicit operator PostLikeModelId((Guid PostId, Guid ProfileId) value)
        {
            return new PostLikeModelId(value.PostId, value.ProfileId);
        }
    }

    public struct PostCommentLikeModelId
    {
        public Guid CommentId;
        public Guid ProfileId;

        public PostCommentLikeModelId(PostCommentModel comment , ProfileModel profile)
            : this(comment.Id, profile.Id) { }

        public PostCommentLikeModelId(Guid commentId, Guid profileId)
        {
            CommentId = commentId;
            ProfileId = profileId;
        }

        public override bool Equals(object? obj)
        {
            return obj is PostCommentLikeModelId other &&
                   CommentId.Equals(other.CommentId) &&
                   ProfileId.Equals(other.ProfileId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CommentId, ProfileId);
        }

        public void Deconstruct(out Guid commentId, out Guid profileId)
        {
            commentId = CommentId;
            profileId = ProfileId;
        }

        public static implicit operator (Guid CommentId, Guid ProfileId)(PostCommentLikeModelId value)
        {
            return (value.CommentId, value.ProfileId);
        }

        public static implicit operator PostCommentLikeModelId((Guid CommentId, Guid ProfileId) value)
        {
            return new PostCommentLikeModelId(value.CommentId, value.ProfileId);
        }
    }
}
