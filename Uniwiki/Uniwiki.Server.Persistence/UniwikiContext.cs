using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence
{
    internal class UniwikiContext : DbContext
    {
        private readonly IServiceProvider _serviceProvider;

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

        public UniwikiContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

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
            options
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            // TODO: Move the connection string to the configuration
            // TODO: Make it possible to switch between the options in configuration
            // options.UseSqlServer("Server=localhost\\SQLEXPRESS01; initial catalog=UniwikiLocalDatabase;Database=master;Trusted_Connection=True; integrated security=SSPI");
            options.UseInMemoryDatabase("UniwikiLocalDatabase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Get all the mapping of entities
            var modelMaps = _serviceProvider.GetServices(typeof(IModelMapBase));

            // Add all the maps to the context
            foreach (var builder in modelMaps)
            {
                ((IModelMapBase)builder).Map(modelBuilder);
            }
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
}
