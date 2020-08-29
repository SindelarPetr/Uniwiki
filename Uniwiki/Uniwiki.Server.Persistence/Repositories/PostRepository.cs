using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared;
using Uniwiki.Server.Application.Configuration;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    public class PostRepository : RemovableRepositoryBase<PostModel, Guid>
    {
        private readonly UniwikiContext _uniwikiContext;
        private readonly TextService _textService;
        private readonly UniwikiConfiguration _uniwikiConfiguration;

        public override string NotFoundByIdMessage => _textService.Error_PostNotFound;

        public PostRepository(UniwikiContext uniwikiContext, TextService textService, UniwikiConfiguration uniwikiConfiguration)
            : base(uniwikiContext, uniwikiContext.Posts)
        {
            _uniwikiContext = uniwikiContext;
            _textService = textService;
            _uniwikiConfiguration = uniwikiConfiguration;
        }

        public IQueryable<PostModel> EditPost(PostModel post, string text, string? postType, PostFileModel[] postFiles)
        {
            post.Edit(text, postType, postFiles);

            var updatedPost = FindById(post.Id);

            return updatedPost;
        }

        public IQueryable<PostModel> FetchPosts(Guid courseId, int? lastPostNumber, int requestPostsToFetch) 
            => FetchPosts(courseId, false, null, lastPostNumber, requestPostsToFetch);

        public IQueryable<PostModel> FetchPostsOfPostType(Guid courseId, string? postType, int? lastPostNumber, int requestPostsToFetch) 
            => FetchPosts(courseId, true, postType, lastPostNumber, requestPostsToFetch);

        private IQueryable<PostModel> FetchPosts(Guid courseId, bool usePostType, string? postType, int? lastPostNumber, int requestPostsToFetch)
        {
            IQueryable<PostModel> posts = _uniwikiContext
                .Posts
                .Where(p => p.CourseId == courseId)
                .OrderByDescending(p => p.PostNumber);

            var count = posts.Count();

            if (lastPostNumber != null)
                posts = posts.Where(p => p.PostNumber < lastPostNumber.Value);

            if (usePostType)
                posts = posts.Where(p => p.PostType == postType);

            return posts.Take(requestPostsToFetch);
        }

        public bool CanFetchMoreOfPostType(Guid courseId, string? postType, int? lastPostNumber) 
            => FetchPostsOfPostType(courseId, postType, lastPostNumber, 1).Any();

        public bool CanFetchMore(Guid courseId, int? lastPostNumber) 
            => FetchPosts(courseId, lastPostNumber, 1).Any();

        public PostModel AddPost(string? postType, Guid profileId, string text, Guid courseId, DateTime creationTime)
        {
            var post = new PostModel(Guid.NewGuid(), postType, profileId, text, courseId, false, creationTime);

            All.Add(post);

            SaveChanges();

            return post;
        }

        public (string Category, int Count)[] GetFilterCategories(CourseModel course) 
            => All
                .Where(p => p.CourseId == course.Id)
                .GroupBy(p => p.PostType)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToArray()
                .Select(g => (g.Category, g.Count))
                .ToArray();

        public string[] GetNewPostCategories(CourseModel course, Language language)
        {
            var defaultPostCategories = language == Language.Czech ?
                _uniwikiConfiguration.Defaults.DefaultPostCategoriesCz :
                _uniwikiConfiguration.Defaults.DefaultPostCategoriesEn;

            return All
                .Where(p => p.CourseId == course.Id && p.PostType != null)
                .Select(p => p.PostType)
                .Distinct()
                .ToArray()
                .Select(t => t!)
                .Concat(defaultPostCategories)
                .Distinct()
                .ToArray();
        }
    }
}