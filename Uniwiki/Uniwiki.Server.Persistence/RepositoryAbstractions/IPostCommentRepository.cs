using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface IPostCommentRepository : IRemovableRepository<PostCommentModel>, IIdRepository<PostCommentModel, Guid>
    {
        PostCommentModel EditComment(PostCommentModel comment, string text);
    }
}