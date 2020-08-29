using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Uniwiki.Server.Persistence.ModelIds;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    public class PostLikeRepository : RepositoryBase<PostLikeModel, PostLikeModelId>
    {
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.Error_PostLikeNotFound;

        public PostLikeRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.PostLikes)
        {
            _textService = textService;
        }

        public void LikePost(Guid postId, Guid profileId, DateTime dateTime)
        {
            // Try to find existing like
            var existingLikeId = new PostLikeModelId(postId, profileId);
            var existingLike = All.Find(existingLikeId.GetKeyValues());

            // If there is no like
            if (existingLike == null)
            {
                // Create the like
                var like = new PostLikeModel(postId, profileId, dateTime, true);

                // Save it to the DB
                All.Add(like);
            }
            else
            {
                // If it already is liked
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

        public void UnlikePost(Guid postId, Guid profileId)
        {
            // Try to find an existing like
            var existingLikeId = new PostLikeModelId(postId, profileId);
            var existingLike = All.Find(existingLikeId.GetKeyValues());

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
