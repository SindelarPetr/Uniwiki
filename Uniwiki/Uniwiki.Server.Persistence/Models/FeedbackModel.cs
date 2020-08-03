using System;
using System.Collections.Generic;
using System.Text;

namespace Uniwiki.Server.Persistence.Models
{
    public class FeedbackModel
    {
        public ProfileModel? User { get; protected set; }
        public int? Rating { get; protected set; }
        public string Text { get; protected set; }
        public string IpAddress { get; protected set; }
        public DateTime CreationTime { get; protected set; }

        internal FeedbackModel()
        {

        }

        internal FeedbackModel(ProfileModel? user, int? rating, string text, string ipAddress, DateTime creationTime)
        {
            User = user;
            Rating = rating;
            Text = text;
            IpAddress = ipAddress;
            CreationTime = creationTime;
        }
    }
}
