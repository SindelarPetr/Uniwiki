using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.ComponentModel.DataAnnotations;
using Uniwiki.Server.Persistence.Maps;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence
{
    // TODO: Change the lengths of all the strings in the DB (the default is max size = 4000, most of our things are like 200)
    public class UniwikiContext : DbContext, IDisposable
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

        public UniwikiContext(IServiceProvider serviceProvider, DbContextOptions builder)
        : base(builder)
        {
            _serviceProvider = serviceProvider;
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

            // TODO: Remove this
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.BuildIndexesFromAnnotations();
        }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            // Soft deleting
            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity is ISoftDeletable))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues[PersistenceConstants.IsDeleted] = 0;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues[PersistenceConstants.IsDeleted] = 1;
                        break;
                }
            }
        }
    }
}
