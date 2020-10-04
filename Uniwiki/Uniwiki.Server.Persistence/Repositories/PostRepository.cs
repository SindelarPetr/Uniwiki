using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    public class PostRepository
    {
        private readonly UniwikiContext _uniwikiContext;


        public PostRepository(UniwikiContext uniwikiContext)
        {
            _uniwikiContext = uniwikiContext;
        }

        public IQueryable<PostModel> EditPost(PostModel post, string text, string? postType)
        {
            post.Edit(text, postType);

            var updatedPost = _uniwikiContext.Posts.Where(p => p.Id == post.Id);

            return updatedPost;
        }

        public IQueryable<PostModel> FetchPosts(Guid courseId, DateTime? lastPostCreationTime, int requestPostsToFetch) 
            => FetchPosts(courseId, false, null, lastPostCreationTime, requestPostsToFetch);

        public IQueryable<PostModel> FetchPostsOfPostType(Guid courseId, string? postType, DateTime? lastPostCreationTime, int requestPostsToFetch) 
            => FetchPosts(courseId, true, postType, lastPostCreationTime, requestPostsToFetch);

        private IQueryable<PostModel> FetchPosts(Guid courseId, bool usePostType, string? postType, DateTime? lastPostCreationTime, int requestPostsToFetch)
        {
            var posts = _uniwikiContext
                .Posts
                .Where(p => p.CourseId == courseId);

            if (usePostType)
            {
                posts = posts.Where(p => p.PostType == postType);
            }

            if (lastPostCreationTime != null)
            {
                posts = posts.Where(p => p.CreationTime < lastPostCreationTime.Value);
            }

            posts = posts.OrderByDescending(p => p.CreationTime);

            return posts.Take(requestPostsToFetch);
        }

        public bool CanFetchMoreOfPostType(Guid courseId, string? postType, DateTime? lastPostCreationTime) 
            => FetchPostsOfPostType(courseId, postType, lastPostCreationTime, 1).Any();

        public bool CanFetchMore(Guid courseId, DateTime? lastPostCreationTime) 
            => FetchPosts(courseId, lastPostCreationTime, 1).Any();

        public PostModel AddPost(string? postType, Guid profileId, string text, Guid courseId, DateTime creationTime)
        {
            var post = new PostModel(Guid.NewGuid(), postType, profileId, text, courseId, creationTime);

            _uniwikiContext.Posts.Add(post);

            _uniwikiContext.SaveChanges();

            return post;
        }
    }
}