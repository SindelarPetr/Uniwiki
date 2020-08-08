using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostCommentLikeModel : IIdModel<Guid[]>
    {
        public PostCommentModel Comment { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public DateTime LikeTime { get; protected set; }
        public bool IsLiked { get; protected set; }
        public Guid[] Id => new Guid[] { Comment.Id, Profile.Id };

        internal PostCommentLikeModel(PostCommentModel comment, ProfileModel profile, DateTime likeTime, bool isLiked)
        {
            Comment = comment;
            Profile = profile;
            LikeTime = likeTime;
            IsLiked = isLiked;
        }

        internal PostCommentLikeModel()
        {

        }
    }
}