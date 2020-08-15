using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Server.Persistence;
using Shared;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Repositories.Base;
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
        private static void AddPersistence<TIRepository, TRepository, TModelMap, TModel, TId>(this IServiceCollection services)
            where TIRepository : IRepositoryBase<TModel, TId>
            where TRepository : RepositoryBase<TModel, TId>, TIRepository
            where TModel : ModelBase<TId>
            where TModelMap : ModelMapBase<TModel, TId>
        {
            // To ensure consistency in the project, we will check whether all the types created for an entity have the right consistent type - this means that they are either all removable or all non-removable
            var modelIsRemovable = typeof(TModel) is RemovableModelBase<TId>;
            var mapIsRemovable = typeof(TModelMap) is RemovableModelMapBase<RemovableModelBase<TId>, TId>;
            var repositoryIsRemovable = typeof(TRepository) is RemovableRepositoryBase<RemovableModelBase<TId>, TId>;
            var iRepositoryIsRemovable = typeof(TIRepository) is IRemovableRepositoryBase<RemovableModelBase<TId>, TId>;

            // Are all removable?
            var allAreRemovabel = modelIsRemovable && mapIsRemovable && repositoryIsRemovable && iRepositoryIsRemovable;

            // Are all non-removable?
            var allAreNotRemovable = !modelIsRemovable && !mapIsRemovable && !repositoryIsRemovable && !iRepositoryIsRemovable;

            // Check if either all the types are Removable or none is removable
            if (allAreRemovabel || allAreNotRemovable)
            {
                // Add the types to the services
                services.AddScoped(typeof(TIRepository), typeof(TRepository));
                services.AddTransient(typeof(IModelMapBase), typeof(TModelMap));
            }
            else
            {
                // Inconsistency is found - throw an error
                throw new ArgumentException("There is inconsistency in who is removable and who is not.\n" +
                    $"{typeof(TIRepository).Name} is Removeable: {modelIsRemovable}" +
                    $"{typeof(TRepository).Name} is Removeable: {modelIsRemovable}" +
                    $"{typeof(TModel).Name} is Removeable: {modelIsRemovable}" +
                    $"{typeof(TModelMap).Name} is Removeable: {modelIsRemovable}");
            }
        }


        public static void AddUniwikiServerPersistence(this IServiceCollection services)
        {
            services.AddUniwikiSharedServices();
            services.AddServerPersistence();
            services.AddScoped<TextService>();
            services.AddDbContext<UniwikiContext>();

            // Course
            services.AddPersistence<ICourseRepository, CourseRepository, CourseModelMap, CourseModel, Guid>();

            // CourseVisit
            services.AddPersistence<ICourseVisitRepository, CourseVisitRepository, CourseVisitModelMap, CourseVisitModel, Guid>();

            // EmailConfirmationSecret
            services.AddPersistence<IEmailConfirmationSecretRepository, EmailConfirmationSecretRepository, EmailConfirmationSecretModelMap, EmailConfirmationSecretModel, Guid>();

            // Feedback
            services.AddPersistence<IFeedbackRepository, FeedbackRepository, FeedbackModelMap, FeedbackModel, Guid>();

            // LoginToken
            services.AddPersistence<ILoginTokenRepository, LoginTokenRepository, LoginTokenModelMap, LoginTokenModel, Guid>();

            // NewPasswordSecret
            services.AddPersistence<INewPasswordSecretRepository, NewPasswordSecretRepository, NewPasswordSecretModelMap, NewPasswordSecretModel, Guid>();

            // PostCommentLike
            services.AddPersistence<IPostCommentLikeRepository, PostCommentLikeRepository, PostCommentLikeModelMap, PostCommentLikeModel, PostCommentLikeModelId>();

            // PostComment
            services.AddPersistence<IPostCommentRepository, PostCommentRepository, PostCommentModelMap, PostCommentModel, Guid>();

            // PostFileDownload
            services.AddPersistence<IPostFileDownloadRepository, PostFileDownloadRepository, PostFileDownloadModelMap, PostFileDownloadModel, Guid>();

            // PostFile
            services.AddPersistence<IPostFileRepository, PostFileRepository, PostFileModelMap, PostFileModel, Guid>();

            // PostLike
            services.AddPersistence<IPostLikeRepository, PostLikeRepository, PostLikeModelMap, PostLikeModel, PostLikeModelId>();

            // Post
            services.AddPersistence<IPostRepository, PostRepository, PostModelMap, PostModel, Guid>();

            // Profile
            services.AddPersistence<IProfileRepository, ProfileRepository, ProfileModelMap, ProfileModel, Guid>();

            // StudyGroup
            services.AddPersistence<IStudyGroupRepository, StudyGroupRepository, StudyGroupModelMap, StudyGroupModel, Guid>();

            // University
            services.AddPersistence<IUniversityRepository, UniversityRepository, UniversityModelMap, UniversityModel, Guid>();
        }
    }
}
