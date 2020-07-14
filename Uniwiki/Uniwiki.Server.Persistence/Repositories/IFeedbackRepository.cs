using System;
using System.Collections.Generic;
using System.Text;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence.Repositories
{
    public interface IFeedbackRepository
    {
        void CreateFeedback(ProfileModel? user, int? rating, string text, string ipAddress, DateTime creationTime);
        double? GetAverageRating();
        int GetFeedbacksCount();
        IEnumerable<string> GetLastFeedbacks(int count);
        int GetTextOnlyFeedbacksCount();
        int RatingOnlyFeedbacksCount();
        int TextAndRatingFeedbacksCount();
    }
}
