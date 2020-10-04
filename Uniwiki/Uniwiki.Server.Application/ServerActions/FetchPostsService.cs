using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Server.Application.ServerActions
{
    public class FetchPostsService
    {
        private readonly PostRepository _postRepository;

        public FetchPostsService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public (IEnumerable<PostViewModel> postViewModels, bool canFetchMore) FetchPosts(bool usePostTypeFilter, Guid courseId, string? postType, DateTime? lastPostCreationTime, int postsToFetch, Guid? userId)
        {
            // Get posts for the 
            var posts = !usePostTypeFilter
                ? _postRepository.FetchPosts(courseId, lastPostCreationTime, postsToFetch)
                : _postRepository.FetchPostsOfPostType(courseId, postType, lastPostCreationTime, postsToFetch);

            // Check if can fetch more posts
            var canFetchMore = !usePostTypeFilter
                ? _postRepository.CanFetchMore(courseId, lastPostCreationTime)
                : _postRepository.CanFetchMoreOfPostType(courseId, postType, lastPostCreationTime);

            // Convert posts to DTOs
            var postDtos = posts.ToPostViewModel(userId);

            return (postDtos, canFetchMore);
        }
    }
}
