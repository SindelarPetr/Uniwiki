﻿using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Uniwiki.Server.Persistence.InMemory.Repositories;
using Uniwiki.Server.Persistence.InMemory.Repositories.Authentication;
using Uniwiki.Server.Persistence.InMemory.Services;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Repositories.Authentication;

[assembly: InternalsVisibleTo("Uniwiki.Server.Application.Tests")]
namespace Uniwiki.Server.Persistence.InMemory
{
    public static class UniwikiServerPersistenceInMemoryServices
    {
        public static void AddUniwikiServerPersistenceInMemory(this IServiceCollection services)
        {
            services.AddUniwikiServerPersistence();

            // Add Data Service
            services.AddSingleton<DataService>();

            // Repositories
            services.AddScoped<IEmailConfirmationSecretRepository, EmailConfirmationSecretInMemoryRepository>();
            services.AddScoped<ILoginTokenRepository, LoginTokenInMemoryRepository>();
            services.AddScoped<INewPasswordSecretRepository, NewPasswordSecretInMemoryRepository>();
            services.AddScoped<IProfileRepository, ProfileInMemoryRepository>();
            services.AddScoped<ICourseRepository, CourseInMemoryRepository>();
            services.AddScoped<IPostCommentRepository, PostCommentInMemoryRepository>();
            services.AddScoped<IPostFileRepository, PostFileInMemoryRepository>();
            services.AddScoped<IPostRepository, PostInMemoryRepository>();
            services.AddScoped<IPostTypeRepository, PostTypeInMemoryRepository>();
            services.AddScoped<IStudyGroupRepository, StudyGroupInMemoryRepository>();
            services.AddScoped<IUniversityRepository, UniversityInMemoryRepository>();
            services.AddScoped<ICourseVisitRepository, CourseVisitInMemoryRepository>();
            services.AddScoped<IPostFileDownloadRepository, PostFileDownloadInMemoryRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackInMemoryRepository>();
        }
    }
}
