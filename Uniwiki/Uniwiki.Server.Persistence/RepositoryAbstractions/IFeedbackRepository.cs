﻿using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface IFeedbackRepository : IIdRepository<FeedbackModel, Guid>
    {
        double? GetAverageRating();
        int GetFeedbacksCount();
        IEnumerable<string> GetLastFeedbacks(int count);
        int GetTextOnlyFeedbacksCount();
        int RatingOnlyFeedbacksCount();
        int TextAndRatingFeedbacksCount();
    }
}