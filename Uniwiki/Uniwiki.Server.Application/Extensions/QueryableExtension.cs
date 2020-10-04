using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.Extensions
{
    internal static class QueryableExtension
    {
        public static IQueryable<StudyGroupDto> ToStudyGroupDto(this IQueryable<StudyGroupModel> studyGroups)
            => studyGroups
            .Include(g => g.University)
            .Select(g => new StudyGroupDto(g.Id, g.ShortName, g.LongName, g.Url, g.University.LongName, g.University.ShortName, g.University.Url, g.UniversityId));

        public static IQueryable<CourseDto> ToCourseDto(this IQueryable<CourseModel> courses)
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

        public static IQueryable<PostFileDto> ToPostFileDto(this IQueryable<PostFileModel> postFiles)
            => postFiles
            .Select(f => new PostFileDto(f.Id, f.NameWithoutExtension, f.Extension, f.Size));

        public static ProfileViewModel? ToProfileViewModel(this IQueryable<ProfileModel> profileQuery)
        {
            var p = profileQuery
                    .Include(p => p.HomeFaculty)
                    .ThenInclude(p => p.University)
                    .FirstOrDefault();

            return p == null ? null : 
                new ProfileViewModel(
                p.Id,
                p.FirstName,
                p.FamilyName,
                p.FullName,
                p.ProfilePictureSrc,
                p.CreationDate,
                p.Url,
                (p.Feedbacks?.Count ?? 0) > 0,
                p.HomeFaculty == null
                    ? null
                    : new HomeStudyGroupDto(
                        p.HomeFaculty.Id,
                        p.HomeFaculty.ShortName,
                        p.HomeFaculty.LongName,
                        p.HomeFaculty.University.ShortName,
                        p.HomeFaculty.University.LongName,
                        p.HomeFaculty.Url,
                        p.HomeFaculty.University.Url,
                        p.HomeFaculty.University.Id)
            ); // Include the email just for the owner of the email
        }

        public static UniversityDto ToUniversityDto(this UniversityModel university)
            => new UniversityDto(university.Id, university.LongName, university.ShortName, university.Url);

        public static IQueryable<FoundCourseDto> ToFoundCourses(this IQueryable<CourseModel> courses)
            => courses
                .Select(c => new FoundCourseDto(
                        c.Id,
                        c.UniversityUrl + '/' + c.StudyGroupUrl + '/' + c.Url,
                        $"{ c.StudyGroup.University.ShortName } - { c.StudyGroup.LongName }",
                        c.Code,
                        c.LongName));

        public static IEnumerable<PostViewModel> ToPostViewModel(this IQueryable<PostModel> posts, Guid? userId)
        {
            // Future: This can be optimized
            var postViewModels = posts
                .Include(p => p.Likes)
                .Include(p => p.PostFiles)
                .Include(p => p.Author)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Author)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Likes)
                .AsEnumerable()
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
                            ).ToArray(),
                        p.PostFiles.Count,
                        p.PostType,
                        p.Likes.Count(l => l.IsLiked),
                        p.Likes.Any(l => l.IsLiked && l.ProfileId == userId),
                        p.Comments.Select(
                            c => new PostCommentDto(
                            c.Id,
                            c.CreationTime,
                            c.Likes.Count(l => l.IsLiked),
                            c.Author.Url,
                            c.Text,
                            c.Likes.Any(l => l.IsLiked && l.ProfileId == userId),
                            c.Author.ProfilePictureSrc,
                            c.Author.Url,
                            c.Author.FullName,
                            c.AuthorId
                            )).ToArray(),
                        p.AuthorId,
                        p.Author.FullName,
                        p.Author.Url,
                        p.Author.ProfilePictureSrc
                    )
                )
                .ToList();

            postViewModels
                .ForEach(p => p.PostComments = p.PostComments.OrderBy(c => c.CreationTime)
                    .ToArray());

            return postViewModels;
        }

        public static IQueryable<LoginTokenDto> ToLoginTokenDto(this IQueryable<LoginTokenModel> loginTokens)
            => loginTokens
            .Select(t => new LoginTokenDto(t.ProfileId, t.PrimaryTokenId, t.Expiration, t.SecondaryTokenId));

        public static AuthorizedUserDto ToAuthorizedUserDto(this IQueryable<ProfileModel> profileQuery)
        {
            var profileWithFeedbackProvided = profileQuery
                .Include(p => p.HomeFaculty)
                .ThenInclude(p => p!.University)
                .Select(p => new {Profile = p, FeedbacksCount = p.Feedbacks.Count()})
                .First();

            var profile = profileWithFeedbackProvided.Profile;
                return new AuthorizedUserDto(
                        profile.Id,
                        profile.FirstName,
                        profile.FamilyName,
                        profile.FullName,
                        profile.ProfilePictureSrc,
                        profile.Url,
                        profileWithFeedbackProvided.FeedbacksCount > 0,
                        profile.Email,
                        profile.HomeFaculty == null
                            ? null
                            : new HomeStudyGroupDto(
                                profile.HomeFaculty.Id,
                                profile.HomeFaculty.ShortName,
                                profile.HomeFaculty.LongName,
                                profile.HomeFaculty.University.ShortName,
                                profile.HomeFaculty.University.LongName,
                                profile.HomeFaculty.Url,
                                profile.HomeFaculty.University.Url,
                                profile.HomeFaculty.University.Id)
                    );
        }

        public static IQueryable<RecentCourseDto> ToRecentCourseDto(this IQueryable<CourseModel> recentCourses)
            => recentCourses
                .Include(c => c.StudyGroup)
                .ThenInclude(c => c.University)
                .Select(c =>
                    new RecentCourseDto(
                        c.LongName,
                        c.Code,
                        c.StudyGroupUrl,
                        c.StudyGroup.LongName,
                        c.UniversityUrl,
                        c.StudyGroup.University.ShortName,
                        c.Id,
                        c.Url));
    }
}
