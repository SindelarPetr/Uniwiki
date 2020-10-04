using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;

using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    public class PostCommentRepository
    {
        private readonly UniwikiContext _uniwikiContext;

        public PostCommentRepository(UniwikiContext uniwikiContext)
           // : base(uniwikiContext, uniwikiContext.PostComments)
        {
            _uniwikiContext = uniwikiContext;
        }

        public PostCommentModel EditComment(PostCommentModel comment, string text)
        {
            comment.Edit(text);

            return comment;
        }

        public Guid AddPostComment(Guid authorId, Guid postId, string commentText, DateTime creationTime)
        {
            var postComment = new PostCommentModel(Guid.NewGuid(), authorId, postId, commentText, creationTime);

            // postComment.Likes = new List<PostCommentLikeModel>();

            _uniwikiContext.PostComments.Add(postComment);

            return postComment.Id;
        }
    }
}
