using System;
using System.Collections.Generic;
using Shared;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    //public interface PostRepository : IRemovableRepositoryBase<PostModel, Guid>
    //{
    //    PostModel EditPost(PostModel post, string text, string? postType, PostFileModel[] postFiles);
    //    IEnumerable<PostModel> FetchPosts(Guid courseId, string? postType, Guid? lastPostId, int requestPostsToFetch);
    //    bool CanFetchMore(Guid courseId, string? postType, Guid? lastPostId);
    //    IEnumerable<PostModel> FetchPosts(Guid courseId, Guid? lastPostId, int requestPostsToFetch);
    //    bool CanFetchMore(Guid courseId, Guid? lastPostId);
    //    PostModel AddPost(string? postType, Guid profileId, string text, CourseModel course, DateTime creationTime);
    //    (string Category, int Count)[] GetFilterCategories(CourseModel course);
    //    string[] GetNewPostCategories(CourseModel course, Language language);
    //}
}