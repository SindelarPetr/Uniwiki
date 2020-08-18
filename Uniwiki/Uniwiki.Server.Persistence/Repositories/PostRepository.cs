using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Exceptions;
using Uniwiki.Server.Application.Configuration;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class PostRepository : RemovableRepositoryBase<PostModel, Guid>, IPostRepository
    {
        private readonly TextService _textService;
        private readonly UniwikiConfiguration _uniwikiConfiguration;

        public override string NotFoundByIdMessage => _textService.Error_PostNotFound;

        public PostRepository(UniwikiContext uniwikiContext, TextService textService, UniwikiConfiguration uniwikiConfiguration)
            : base(uniwikiContext, uniwikiContext.Posts)
        {
            _textService = textService;
            _uniwikiConfiguration = uniwikiConfiguration;
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
            var posts = All
                .Where(p => p.CourseId == course.Id);

            if (lastPost != null)
                posts = posts.Where(p => p.CreationTime < lastPost.CreationTime);

            // TODO: Optimize
            return posts
                .OrderByDescending(p => p.CreationTime)
                .Take(requestPostsToFetch)
                .Include(p => p.Likes)
                .Include(p => p.PostFiles)
                .Include(p => p.Author)
                .Include(p => p.Comments);
        }

        public bool CanFetchMore(CourseModel course, PostModel? lastPost)
        {
            return FetchPosts(course, lastPost, 1).Any();
        }

        public PostModel AddPost(string? postType, ProfileModel profile, string text, CourseModel course, DateTime creationTime)
        {
            var post = new PostModel(Guid.NewGuid(), postType, profile, text, course, creationTime, false);

            All.Add(post);

            SaveChanges();

            return post;
        }

        public (string Category, int Count)[] GetFilterCategories(CourseModel course)
        {
            return All
                .Where(p => p.CourseId == course.Id)
                .GroupBy(p => p.PostType)
                .Select(g =>  new { Category = g.Key, Count = g.Count() })
                .ToArray()
                .Select(g => (g.Category, g.Count))
                .ToArray();
        }

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