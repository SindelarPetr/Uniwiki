using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Authentication;

namespace Uniwiki.Server.Persistence.SqlServer
{
    public class TestModel
    {
        public string TestText { get; set; }

        public Guid Id { get; set; }
    }

    public class UniwikiContext : DbContext
    {
        public DbSet<LoginTokenModel> LoginTokens { get; set; }
        public DbSet<NewPasswordSecretModel> NewPasswordSecrets { get; set; }
        public DbSet<EmailConfirmationSecretModel> EmailConfirmationSecrets { get; set; }
        public DbSet<ProfileModel> Profiles { get; set; }
        public DbSet<UniversityModel> Universities { get; set; }
        public DbSet<StudyGroupModel> StudyGroups { get; set; }
        public DbSet<CourseVisitModel> CourseVisits { get; set; }
        public IEnumerable<CourseModel> Courses => _courses.OrderBy(c => c.FullName);
        public DbSet<CourseModel> _courses { get; set; }
        public IEnumerable<PostModel> Posts => _posts.Where(p => !p.IsRemoved);
        public DbSet<PostModel> _posts { get; set; }
        public DbSet<PostFileModel> PostFiles { get; set; }
        public IEnumerable<PostCommentModel> PostComments => _postComments.Where(c => !c.IsRemoved);
        public DbSet<PostCommentModel> _postComments { get; set; }
        public IEnumerable<PostLikeModel> PostLikes => _postLikes.Where(l => !l.IsRemoved);
        public DbSet<PostLikeModel> _postLikes { get; set; }
        public IEnumerable<PostCommentLikeModel> PostCommentLikes => _postCommentLikes.Where(c => !c.IsRemoved);
        public DbSet<PostCommentLikeModel> _postCommentLikes { get; set; }
        public IEnumerable<PostFileDownload> PostFileDownloads => _postFileDownloads.OrderBy(d => d.DownloadTime);
        public DbSet<PostFileDownload> _postFileDownloads { get; set; }
        public DbSet<FeedbackModel> Feedbacks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);

            // TODO: Move the connection string to the configuration
            options.UseSqlServer("Server=localhost\\SQLEXPRESS01; initial catalog=UniwikiLocalDatabase;Database=master;Trusted_Connection=True; integrated security=SSPI");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<LoginTokenModel>().
        }
    }
}
