using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence
{
    internal class UniwikiContext : DbContext
    {
        public DbSet<LoginTokenModel> LoginTokens { get; set; }

        public DbSet<NewPasswordSecretModel> NewPasswordSecrets { get; set; }

        public DbSet<EmailConfirmationSecretModel> EmailConfirmationSecrets { get; set; }

        public DbSet<ProfileModel> Profiles { get; set; }

        public DbSet<UniversityModel> Universities { get; set; }

        public DbSet<StudyGroupModel> StudyGroups { get; set; }

        public DbSet<CourseVisitModel> CourseVisits { get; set; }

        public DbSet<CourseModel> Courses { get; set; }

        public DbSet<PostModel> Posts { get; set; }

        // TODO: Create the post file to be removable
        // TODO: Keep track of every post file, which is not used
        public DbSet<PostFileModel> PostFiles { get; set; }

        public DbSet<PostCommentModel> PostComments { get; set; }

        public DbSet<PostLikeModel> PostLikes { get; set; }

        public DbSet<PostCommentLikeModel> PostCommentLikes { get; set; }

        public DbSet<PostFileDownloadModel> PostFileDownloads { get; set; }

        public DbSet<FeedbackModel> Feedbacks { get; set; }

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

            //modelBuilder.Entity<LoginTokenModel>().
        }

    }
}
