using System;
using Uniwiki.Server.Persistence.ModelIds;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.Repositories
{
    public interface IPostCommentLikeRepository : IRepositoryBase<PostCommentLikeModel, PostCommentLikeModelId>
    {
        void LikeComment(PostCommentModel comment, ProfileModel profile, DateTime likeTime);
        void UnlikeComment(PostCommentModel comment, ProfileModel profile);
    }
}
