using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniwiki.Server.Persistence.ModelIds;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{

    public class PostCommentLikeRepository : RepositoryBase<PostCommentLikeModel, PostCommentLikeModelId>
    {
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.Error_PostCommentLikeNotFound;

        public PostCommentLikeRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.PostCommentLikes)
        {
            _textService = textService;
        }

        /// <summary>
        /// Likes the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="likeTime">The like time.</param>
        /// <returns>The ID of the liked post</returns>
        public Guid? LikeComment(Guid commentId, Guid profileId, DateTime likeTime)
        {
            // Try to find an existing like
            var existingLike = All.Include(l => l.Comment).Single(l => l.CommentId == commentId && l.ProfileId == profileId);

            // Check if there already is a like
            if (existingLike == null)
            {
                // Create a new like 
                var newLike = new PostCommentLikeModel(commentId, profileId, likeTime, true);

                // Add it to the DB
                All.Add(newLike);

                // Find the comment post ID
                var postId = UniwikiContext
                    .PostComments
                    .Where(c => c.Id == commentId)
                    .Select(c => c.PostId)
                    .Single();

                return postId;
            }

            // Check if it is already liked
            if (existingLike.IsLiked)
            {
                // Do nothing
                return null;
            }

            // Like it
            existingLike.Like();

            return existingLike.Comment.PostId;

        }

        /// <summary>
        /// Unlikes the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <returns>The post Id to which belongs the liked comment.</returns>
        public Guid? UnlikeComment(Guid commentId, Guid profileId)
        {
            //// Try to find an existing like
            //var existingLikeId = new PostCommentLikeModelId(commentId, profileId).GetKeyValues();

            // Try to get the like
            var existingLike = All.Include(l => l.Comment).Single(l => l.CommentId == commentId && l.ProfileId == profileId);

            // Check if there already is a like or its already unliked
            if (existingLike == null || existingLike.IsLiked == false)
            {
                // Do nothing
                return null;
            }

            // Unlike it
            existingLike.Unlike();

            return existingLike.Comment.PostId;
        }
    }
}
