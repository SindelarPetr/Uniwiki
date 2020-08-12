using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class FeedbackModel : RemovableModelBase<Guid>
    {
        public ProfileModel? User { get; protected set; }
        public int? Rating { get; protected set; }
        public string Text { get; protected set; }
        public DateTime CreationTime { get; protected set; }

        internal FeedbackModel(Guid id, bool isRemoved, ProfileModel? user, int? rating, string text, DateTime creationTime)
            :base(isRemoved, id)
        {
            User = user;
            Rating = rating;
            Text = text;
            CreationTime = creationTime;
        }

        protected FeedbackModel()
        {

        }
    }
}
