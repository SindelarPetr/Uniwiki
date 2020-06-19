using System;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence.Repositories
{
    public interface IPostCommentRepository
    {
        PostCommentModel CreatePostComment(ProfileModel user, PostModel post, string requestCommentText, DateTime creationTime);
        PostCommentModel FindById(Guid id);
        void RemoveComment(PostCommentModel comment);
        PostCommentModel EditComment(PostCommentModel comment, string text);
        void LikeComment(PostCommentModel comment, ProfileModel profile, DateTime likeTime);
        void UnlikeComment(PostCommentModel comment, ProfileModel profile);
    }
}