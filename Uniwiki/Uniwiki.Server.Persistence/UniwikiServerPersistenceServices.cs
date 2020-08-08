using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;

[assembly: InternalsVisibleTo("Uniwiki.Tests")]
[assembly: InternalsVisibleTo("Uniwiki.Server.Persistence.InMemory")]

namespace Uniwiki.Server.Persistence
{
    public static class UniwikiServerPersistenceServices
    {
        public static void AddUniwikiServerPersistence(this IServiceCollection services)
        {
            services.AddServerPersistence();
            services.AddScoped<TextService>();
            services.AddScoped<UniwikiContext>();

            services.AddScoped<IEmailConfirmationSecretRepository, EmailConfirmationSecretRepository>();
            services.AddScoped<ILoginTokenRepository, LoginTokenRepository>();
            services.AddScoped<INewPasswordSecretRepository, NewPasswordSecretRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IPostCommentRepository, PostCommentRepository>();
            services.AddScoped<IPostFileRepository, PostFileRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostTypeRepository, PostTypeRepository>();
            services.AddScoped<IStudyGroupRepository, StudyGroupRepository>();
            services.AddScoped<IUniversityRepository, UniversityRepository>();
            services.AddScoped<ICourseVisitRepository, CourseVisitRepository>();
            services.AddScoped<IPostFileDownloadRepository, PostFileDownloadRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        }
    }
}
