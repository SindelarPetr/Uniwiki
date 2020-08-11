using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.Repositories
{
    public interface IPostLikeRepository : IIdRepository<PostLikeModel, Guid[]>
    {
        void LikePost(PostModel post, ProfileModel profile, DateTime dateTime);
        void UnlikePost(PostModel post, ProfileModel profile);
    }
}