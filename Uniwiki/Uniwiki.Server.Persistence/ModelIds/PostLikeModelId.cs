using System;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence.ModelIds
{
    public struct PostLikeModelId
    {
        public Guid PostId;
        public Guid ProfileId;

        public PostLikeModelId(PostModel post, ProfileModel profile)
            : this(post.Id, profile.Id) { }

        public PostLikeModelId(Guid postId, Guid profileId)
        {
            PostId = postId;
            ProfileId = profileId;
        }

        public override bool Equals(object? obj)
        {
            return obj is PostLikeModelId other &&
                   PostId.Equals(other.PostId) &&
                   ProfileId.Equals(other.ProfileId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PostId, ProfileId);
        }

        public void Deconstruct(out Guid postId, out Guid profileId)
        {
            postId = PostId;
            profileId = ProfileId;
        }

        internal object[] GetKeyValues()
        {
            return new object[] { PostId, ProfileId };
        }

        public static implicit operator (Guid PostId, Guid ProfileId)(PostLikeModelId value)
        {
            return (value.PostId, value.ProfileId);
        }

        public static implicit operator PostLikeModelId((Guid PostId, Guid ProfileId) value)
        {
            return new PostLikeModelId(value.PostId, value.ProfileId);
        }
    }
}
