using System;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Persistence;
using Shared;
using Uniwiki.Server.Persistence.Maps;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.ModelIds;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Base;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Repositories.Base;


using Uniwiki.Server.Persistence.Services;
using Uniwiki.Shared;
using Uniwiki.Shared.Services;

[assembly: InternalsVisibleTo("Uniwiki.Tests")]
[assembly: InternalsVisibleTo("Uniwiki.Server.Application.Tests")]
[assembly: InternalsVisibleTo("Uniwiki.Server.Persistence.Tests")]

namespace Uniwiki.Server.Persistence
{
    public static class UniwikiServerPersistenceServices
    {
        public static void AddUniwikiServerPersistence(this IServiceCollection services, Action<DbContextOptionsBuilder> builder)
        {
            services.AddUniwikiSharedServices();
            services.AddServerPersistence();
            services.AddScoped<TextService>();
            services.AddDbContext<UniwikiContext>(builder);

            // Course
            services.AddScoped<CourseRepository>();

            // CourseVisit
            services.AddScoped<CourseVisitRepository>();

            // EmailConfirmationSecret
            services.AddScoped<EmailConfirmationSecretRepository>();

            // Feedback
            services.AddScoped<FeedbackRepository>();

            // LoginToken
            services.AddScoped<LoginTokenRepository>();

            // NewPasswordSecret
            services.AddScoped<NewPasswordSecretRepository>();

            // PostCommentLike
            services.AddScoped<PostCommentLikeRepository>();

            // PostComment
            services.AddScoped<PostCommentRepository>();

            // PostFileDownload
            services.AddScoped<PostFileDownloadRepository>();

            // PostFile
            services.AddScoped<PostFileRepository>();

            // PostLike
            services.AddScoped<PostLikeRepository>();

            // Post
            services.AddScoped<PostRepository>();

            // Profile
            services.AddScoped<ProfileRepository>();

            // StudyGroup
            services.AddScoped<StudyGroupRepository>();

            // University
            services.AddScoped<UniversityRepository>();

        }
    }
}
