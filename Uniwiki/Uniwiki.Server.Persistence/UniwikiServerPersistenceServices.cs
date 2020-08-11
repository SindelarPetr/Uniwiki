using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Server.Persistence;
using Shared;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;
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
        public static void AddUniwikiServerPersistence(this IServiceCollection services)
        {
            services.AddUniwikiSharedServices();
            services.AddServerPersistence();
            services.AddScoped<TextService>();
            services.AddDbContext<UniwikiContext>();

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseVisitRepository, CourseVisitRepository>();
            services.AddScoped<IEmailConfirmationSecretRepository, EmailConfirmationSecretRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<ILoginTokenRepository, LoginTokenRepository>();
            services.AddScoped<INewPasswordSecretRepository, NewPasswordSecretRepository>();
            services.AddScoped<IPostCommentLikeRepository, PostCommentLikeRepository>();
            services.AddScoped<IPostCommentRepository, PostCommentRepository>();
            services.AddScoped<IPostFileDownloadRepository, PostFileDownloadRepository>();
            services.AddScoped<IPostFileRepository, PostFileRepository>();
            services.AddScoped<IPostLikeRepository, PostLikeRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            //services.AddScoped<IPostTypeRepository, PostTypeRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IStudyGroupRepository, StudyGroupRepository>();
            services.AddScoped<IUniversityRepository, UniversityRepository>();
        }
    }
}
