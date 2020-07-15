using System.Runtime.CompilerServices;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Shared.Standardizers;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.RequestResponse.Authentication;
using Uniwiki.Shared.Standardizers;
using Uniwiki.Shared.Validators;

//[assembly: AssemblyVersion("1.0.*")]
[assembly: InternalsVisibleTo("Uniwiki.Shared.Tests")]
namespace Uniwiki.Shared
{
    public static class UniwikiSharedServices
    {
        public static void AddUniwikiSharedServices(this IServiceCollection services)
        {
            services.AddSharedServices();
            services.AddValidatorsAndStandardizers();
            services.AddSingleton<Constants>();
        }

        private static void AddValidatorsAndStandardizers(this IServiceCollection services)
        {
            services.AddScoped<IStandardizer<AddCourseRequestDto>, AddCourseRequestStandardizer>();
            services.AddScoped<IValidator<AddCourseRequestDto>, AddCourseRequestValidator>();

            services.AddScoped<IStandardizer<AddStudyGroupRequestDto>, AddStudyGroupRequestStandardizer>();
            services.AddScoped<IValidator<AddStudyGroupRequestDto>, AddStudyGroupRequestValidator>();

            services.AddScoped<IStandardizer<LoginRequestDto>, LoginRequestStandardizer>();
            services.AddScoped<IValidator<LoginRequestDto>, LoginRequestValidator>();

            services.AddScoped<IStandardizer<ChangePasswordRequestDto>, ChangePasswordRequestStandardizer>();
            services.AddScoped<IValidator<ChangePasswordRequestDto>, ChangePasswordRequestValidator>();

            services.AddScoped<IStandardizer<CreateNewPasswordRequestDto>, CreateNewPasswordRequestStandardizer>();
            services.AddScoped<IValidator<CreateNewPasswordRequestDto>, CreateNewPasswordRequestValidator>();

            services.AddScoped<IStandardizer<RegisterRequestDto>, RegisterRequestStandardizer>();
            services.AddScoped<IValidator<RegisterRequestDto>, RegisterRequestValidator>();

            services.AddScoped<IStandardizer<RestorePasswordRequestDto>, RestorePasswordRequestStandardizer>();
            services.AddScoped<IValidator<RestorePasswordRequestDto>, RestorePasswordRequestValidator>();

            services.AddScoped<IStandardizer<IsEmailAvailableRequestDto>, IsEmailAvailableRequestStandardizer>();
            services.AddScoped<IValidator<IsEmailAvailableRequestDto>, IsEmailAvailableRequestValidator>();

            services.AddScoped<IValidator<EditPostRequestDto>, EditPostRequestValidator>();

            services.AddScoped<IValidator<PostFileDto>, PostFileValidator>();

            services.AddScoped<IStandardizer<AddPostRequestDto>, AddPostRequestStandardizer>();
        }
    }
}
