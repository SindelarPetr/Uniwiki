using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence.Repositories
{
    public interface IPostRepository
    {
        PostModel FindById(Guid postId);
        PostModel EditPost(PostModel post, string text, string? postType, PostFileModel[] postFiles);
        IEnumerable<PostModel> FetchPosts(CourseModel course, string? postType, PostModel? lastPost, int requestPostsToFetch);
        bool CanFetchMore(CourseModel course, string? postType, PostModel? lastPost);
        IEnumerable<PostModel> FetchPosts(CourseModel course, PostModel? lastPost, int requestPostsToFetch);
        bool CanFetchMore(CourseModel course, PostModel? lastPost);
        void LikePost(PostModel post, ProfileModel profile, DateTime likeTime);
        void UnlikePost(PostModel post, ProfileModel user);
        void RemovePost(PostModel post);
        PostModel CreatePost(string? postType, ProfileModel profile, string text, CourseModel course, DateTime creationTime, IEnumerable<PostFileModel> files);
    }
}