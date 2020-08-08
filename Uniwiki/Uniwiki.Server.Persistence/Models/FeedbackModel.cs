﻿using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class FeedbackModel : IIdModel<Guid>, IRemovableModel
    {
        public ProfileModel? User { get; protected set; }
        public int? Rating { get; protected set; }
        public string Text { get; protected set; }
        public string IpAddress { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        bool IRemovableModel.IsRemoved { get; set; }
        public Guid Id { get; protected set; }

        internal FeedbackModel(Guid id, bool isRemoved, ProfileModel? user, int? rating, string text, string ipAddress, DateTime creationTime)
        {
            Id = id;
            User = user;
            Rating = rating;
            Text = text;
            IpAddress = ipAddress;
            CreationTime = creationTime;
            ((IRemovableModel)this).IsRemoved = isRemoved;
        }
        
        internal FeedbackModel()
        {

        }
    }
}
