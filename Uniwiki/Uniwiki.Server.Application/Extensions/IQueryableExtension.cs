using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.Extensions
{
    static class IQueryableExtension
    {
        public static IQueryable<StudyGroupDto> ToDto(this IQueryable<StudyGroupModel> studyGroups)
            => studyGroups
            .Include(g => g.University)
            .Select(g => new StudyGroupDto(g.Id, g.ShortName, g.LongName, g.Url, g.University.LongName, g.University.ShortName, g.University.Url, g.UniversityId));

        public static IQueryable<CourseDto> ToDto(this IQueryable<CourseModel> courses)
            => courses
            .Include(c => c.StudyGroup)
            .ThenInclude(g => g.University)
            .Select(c => new CourseDto(
                c.Id,
                c.LongName,
                c.Code,
                c.Url,
                c.StudyGroup.LongName,
                c.StudyGroup.Url,
                c.StudyGroup.University.ShortName,
                c.StudyGroup.University.Url)
            );

        public static IQueryable<PostFileDto> ToDto(this IQueryable<PostFileModel> postFiles)
            => postFiles
            .Select(f => new PostFileDto(f.Id, f.NameWithoutExtension, f.Extension, f.Size));

        public static IQueryable<ProfileViewModel> ToViewModel(this IQueryable<ProfileModel> profiles, Guid? userId)
            => profiles
            .Include(p => p.HomeFaculty)
            .Select(p => new ProfileViewModel(
                p.Id,
                p.FirstName,
                p.FamilyName,
                p.FullName,
                p.ProfilePictureSrc,
                p.CreationDate,
                p.Url,
                p.Feedbacks.Any(),
                p.HomeFaculty != null ? p.HomeFaculty.Url : null,
                p.HomeFaculty != null ? p.HomeFaculty.LongName : null,
                p.HomeFacultyId,
                userId == p.Id ? p.Email : null) // Include the email just for the owner of the email
            );

        public static UniversityDto ToDto(this UniversityModel university)
            => new UniversityDto(university.Id, university.LongName, university.ShortName, university.Url);

        public static IQueryable<FoundCourseDto> ToFoundCourses(this IQueryable<CourseModel> courses)
            => courses
                    .Include(c => c.StudyGroup)
                    .ThenInclude(g => g.University)
                    .Select(c => new FoundCourseDto(
                        c.Id,
                        c.UniversityUrl + '/' + c.StudyGroupUrl + '/' + c.Url,
                        $"{ c.StudyGroup.University.ShortName } - { c.StudyGroup.LongName }",
                        c.Code,
                        c.LongName));

        // TODO: Tune the performance - check which SQL requests are being made
        public static IQueryable<PostViewModel> ToDto(this IQueryable<PostModel> posts, Guid? profileId) =>
            posts
                .Include(p => p.Likes)
                .Include(p => p.PostFiles)
                .Include(p => p.Author)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Author)
                .Select(p =>
                    new PostViewModel(
                        p.Id,
                        p.Author.Url,
                        p.Text,
                        p.CreationTime,
                        p.PostFiles.Select(pf => new PostFileDto(
                            pf.Id,
                            pf.NameWithoutExtension,
                            pf.Extension,
                            pf.Size)
                            ),
                        p.PostFiles.Count(),
                        p.PostType,
                        p.Likes.Count(),
                        p.Likes.Any(l => l.ProfileId == p.Id),
                        p.Comments.Select(c => new PostCommentDto(
                            c.Id,
                            c.CreationTime,
                            c.Likes.Count(),
                            c.Author.Url,
                            c.Text,
                            c.Likes.Any(l => l.ProfileId == profileId),
                            c.Author.ProfilePictureSrc,
                            c.Author.Url,
                            c.Author.FullName,
                            c.AuthorId
                            )).ToArray(), // TODO: Check how this is going to perform thanks of this ToArray()
                        p.AuthorId,
                        p.Author.FullName,
                        p.Author.Url,
                        p.Author.ProfilePictureSrc,
                        p.PostNumber
                        )
                    );

        public static IQueryable<LoginTokenDto> ToDto(this IQueryable<LoginTokenModel> loginTokens)
            => loginTokens
            .Select(t => new LoginTokenDto(t.ProfileId, t.PrimaryTokenId, t.Expiration, t.SecondaryTokenId));

        public static IQueryable<AuthorizedUserDto> ToAuthorizedUser(this IQueryable<ProfileModel> profiles)
            => profiles
            .Include(p => p.HomeFaculty)
            .Select(p => new AuthorizedUserDto(
                p.Id,
                p.FirstName,
                p.FamilyName,
                p.FullName,
                p.ProfilePictureSrc,
                p.Url,
                p.FeedbackProvided,
                p.HomeFaculty != null ? p.HomeFaculty.Url : null,
                p.HomeFaculty != null ? p.HomeFaculty.LongName : null,
                p.HomeFacultyId,
                p.Email,
                p.HomeFaculty != null ? p.HomeFaculty.ShortName : null
                )
            );
    }
}
