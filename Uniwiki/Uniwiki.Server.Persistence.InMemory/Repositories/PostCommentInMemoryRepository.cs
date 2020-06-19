using System;
using System.Linq;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.InMemory.Services;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;

namespace Uniwiki.Server.Persistence.InMemory.Repositories
{
    internal class PostCommentInMemoryRepository : IPostCommentRepository
    {
        private readonly DataService _dataService;

        public PostCommentInMemoryRepository(DataService dataService)
        {
            _dataService = dataService;
        }

        public PostCommentModel CreatePostComment(ProfileModel profile, PostModel post, string text, DateTime creationTime)
        {
            var id = Guid.NewGuid();
            var likes = _dataService.PostCommentLikes.Where(l => l.Comment.Id == id);
            var comment = new PostCommentModel(id, profile, post, text, creationTime, likes);
            _dataService._postComments.Add(comment);
            return comment;
        }

        public PostCommentModel FindById(Guid id)
        {
            return _dataService.PostComments.FirstOrDefault(c => !c.IsRemoved && c.Id == id) 
                   ?? throw new RequestException("Cannot find the specified comment.");
        }

        public void RemoveComment(PostCommentModel comment)
        {
            comment.Remove();
        }

        public PostCommentModel EditComment(PostCommentModel comment, string text)
        {
            comment.Edit(text);
            return comment;
        }

        public void LikeComment(PostCommentModel comment, ProfileModel profile, DateTime likeTime)
        {
            var newLike = new PostCommentLikeModel(comment, profile, likeTime);
            _dataService._postCommentLikes.Add(newLike);
        }

        public void UnlikeComment(PostCommentModel comment, ProfileModel profile) 
            => comment.Likes.FirstOrDefault(l => l.Profile == profile)?.Remove();
    }
}
