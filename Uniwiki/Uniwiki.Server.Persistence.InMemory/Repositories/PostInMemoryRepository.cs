using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.InMemory.Services;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.InMemory.Repositories
{
    internal class PostInMemoryRepository : IPostRepository
    {
        private readonly DataService _dataService;
        private readonly TextService _textService;

        public PostInMemoryRepository(DataService dataService, TextService textService)
        {
            _dataService = dataService;
            _textService = textService;
        }

        public PostModel CreatePost(string? postType, ProfileModel profile, string text, CourseModel course, DateTime creationTime, IEnumerable<PostFileModel> files)
        {
            // Generate id
            var id = Guid.NewGuid();

            // Get comments
            var comments = _dataService.PostComments.Where(c => c.Post.Id == id);

            // Get likes
            var likes = _dataService.PostLikes.Where(l => l.Post.Id == id);

            // Create model
            var post = new PostModel(id, postType, profile, text, course, creationTime, files, comments, likes);

            // Add the model to the DB
            _dataService._posts.Add(post);

            return post;
        }

        public PostModel EditPost(PostModel post, string text, string? postType, PostFileModel[] postFiles)
        {
            post.Edit(text, postType, postFiles);

            return post;
        }

        public IEnumerable<PostModel> FetchPosts(CourseModel course, string? postType, PostModel? lastPost, int requestPostsToFetch)
        {
            var posts = course.Posts.Where(p => p.PostType == postType).Reverse();
            if (lastPost != null)
                posts = posts.SkipWhile(p => p != lastPost).Skip(1);
            return posts.Take(requestPostsToFetch);
        }

        public bool CanFetchMore(CourseModel course, string? postType, PostModel? lastPost)
        {
            return FetchPosts(course, postType, lastPost, 1).Any();
        }

        public IEnumerable<PostModel> FetchPosts(CourseModel course, PostModel? lastPost, int requestPostsToFetch)
        {
            var posts = _dataService.Posts.Where(p => p.Course == course).Reverse();
            if (lastPost != null)
                posts = posts.SkipWhile(p => p != lastPost).Skip(1);
            return posts.Take(requestPostsToFetch);
        }

        public bool CanFetchMore(CourseModel course, PostModel? lastPost)
        {
            return FetchPosts(course, lastPost, 1).Any();
        }

        public void LikePost(PostModel post, ProfileModel profile, DateTime likeTime)
        {
            var like = new PostLikeModel(post, profile, likeTime);
            _dataService._postLikes.Add(like);
        }

        public void UnlikePost(PostModel post, ProfileModel user)
        {
            post.Likes.FirstOrDefault(l => l.Profile == user)?.Removed();
        }

        public void RemovePost(PostModel post)
        {
            post.Remove();
        }

        public PostModel FindById(Guid postId)
        {
            return _dataService.Posts.FirstOrDefault(p => p.Id == postId) ?? throw new RequestException(_textService.Error_PostNotFound);
        }

    }
}