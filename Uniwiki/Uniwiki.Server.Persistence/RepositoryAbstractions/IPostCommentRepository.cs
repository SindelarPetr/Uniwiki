using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface IPostCommentRepository : IRemovableRepositoryBase<PostCommentModel, Guid>
    {
        PostCommentModel EditComment(PostCommentModel comment, string text);
        PostCommentModel AddPostComment(ProfileModel profile, PostModel post, string commentText, DateTime creationTime);
    }
}