using System;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence.ModelIds
{
    public struct PostCommentLikeModelId
    {
        public Guid CommentId;
        public Guid ProfileId;

        public PostCommentLikeModelId(PostCommentModel comment, ProfileModel profile)
            : this(comment.Id, profile.Id) { }

        public PostCommentLikeModelId(Guid commentId, Guid profileId)
        {
            CommentId = commentId;
            ProfileId = profileId;
        }

        public override bool Equals(object? obj)
        {
            return obj is PostCommentLikeModelId other &&
                   CommentId.Equals(other.CommentId) &&
                   ProfileId.Equals(other.ProfileId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CommentId, ProfileId);
        }

        public void Deconstruct(out Guid commentId, out Guid profileId)
        {
            commentId = CommentId;
            profileId = ProfileId;
        }

        public static implicit operator (Guid CommentId, Guid ProfileId)(PostCommentLikeModelId value)
        {
            return (value.CommentId, value.ProfileId);
        }

        internal object[] GetKeyValues() => new object[] { CommentId, ProfileId };

        public static implicit operator PostCommentLikeModelId((Guid CommentId, Guid ProfileId) value)
        {
            return new PostCommentLikeModelId(value.CommentId, value.ProfileId);
        }
    }
}
