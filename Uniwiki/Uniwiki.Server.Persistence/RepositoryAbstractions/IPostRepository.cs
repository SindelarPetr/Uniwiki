using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface IPostRepository : IIdRepository<PostModel, Guid>, IRemovableRepository<PostModel>
    {
        PostModel EditPost(PostModel post, string text, string? postType, PostFileModel[] postFiles);
        IEnumerable<PostModel> FetchPosts(CourseModel course, string? postType, PostModel? lastPost, int requestPostsToFetch);
        bool CanFetchMore(CourseModel course, string? postType, PostModel? lastPost);
        IEnumerable<PostModel> FetchPosts(CourseModel course, PostModel? lastPost, int requestPostsToFetch);
        bool CanFetchMore(CourseModel course, PostModel? lastPost);
    }
}