using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{

    class PostCommentLikeRepository : RepositoryBase<PostCommentLikeModel, PostCommentLikeModelId>, IPostCommentLikeRepository
    {
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.Error_PostCommentLikeNotFound;

        public PostCommentLikeRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.PostCommentLikes)
        {
            _textService = textService;
        }

        public void LikeComment(PostCommentModel comment, ProfileModel profile, DateTime likeTime)
        {
            // Try to find an existing like
            var existingLikeId = new PostCommentLikeModelId(comment, profile);
            var existingLike = All.Find(existingLikeId);

            // Check if there already is a like
            if (existingLike == null)
            {
                // TODO: Create a new like 
                var newLike = new PostCommentLikeModel(comment, profile, likeTime, true);

                // Add it to the DB
                All.Add(newLike);
            }
            else
            {
                // Check if it is already liked
                if (existingLike.IsLiked)
                {
                    // Do nothing
                    return;
                }

                // Like it
                existingLike.Like();
            }

            SaveChanges();
        }

        public void UnlikeComment(PostCommentModel comment, ProfileModel profile)
        {
            // Try to find an existing like
            var existingLikeId = new PostCommentLikeModelId(comment, profile);

            // 
            var existingLike = All.Find(existingLikeId);

            // Check if there already is a like or its already unliked
            if (existingLike == null || existingLike.IsLiked == false)
            {
                // Do nothing
                return;
            }

            // Unlike it
            existingLike.Unlike();

            SaveChanges();
        }
    }
}
