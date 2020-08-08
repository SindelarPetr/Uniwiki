using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface IPostCommentRepository : IRemovableRepository<PostCommentModel>, IIdRepository<PostCommentModel, Guid>
    {
        //PostCommentModel CreatePostComment(ProfileModel user, PostModel post, string requestCommentText, DateTime creationTime);
        PostCommentModel EditComment(PostCommentModel comment, string text);
        void LikeComment(PostCommentModel comment, ProfileModel profile, DateTime likeTime);
        void UnlikeComment(PostCommentModel comment, ProfileModel profile);
    }
}