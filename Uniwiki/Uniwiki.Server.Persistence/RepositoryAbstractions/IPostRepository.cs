using System;
using System.Collections.Generic;
using Shared;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface IPostRepository : IRemovableRepositoryBase<PostModel, Guid>
    {
        PostModel EditPost(PostModel post, string text, string? postType, PostFileModel[] postFiles);
        IEnumerable<PostModel> FetchPosts(CourseModel course, string? postType, Guid? lastPostId, int requestPostsToFetch);
        bool CanFetchMore(CourseModel course, string? postType, Guid? lastPostId);
        IEnumerable<PostModel> FetchPosts(CourseModel course, Guid? lastPostId, int requestPostsToFetch);
        bool CanFetchMore(CourseModel course, Guid? lastPostId);
        PostModel AddPost(string? postType, ProfileModel profile, string text, CourseModel course, DateTime creationTime);
        (string Category, int Count)[] GetFilterCategories(CourseModel course);
        string[] GetNewPostCategories(CourseModel course, Language language);
    }
}