using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class UniversityModel : IRemovableModel, IIdModel<Guid>
    {
        /// <summary>
        /// e. g. 'IT University of Copenhagen'
        /// </summary>
        public string FullName { get; protected set; }

        /// <summary>
        /// e. g. 'ITU'
        /// </summary>
        public string ShortName { get; protected set; }

        public string Url { get; protected set; }

        public IEnumerable<StudyGroupModel> StudyGroups { get; protected set; }
        bool IRemovableModel.IsRemoved { get; set; }

        public Guid Id { get; protected set; }

        internal UniversityModel()
        {

        }

        internal UniversityModel(Guid id, string fullName, string shortName, string url, IEnumerable<StudyGroupModel> studyGroups, bool isRemoved)
        {
            Id = id;
            FullName = fullName;
            ShortName = shortName;
            StudyGroups = studyGroups;
            Url = url;
            ((IRemovableModel)this).IsRemoved = isRemoved;
        }
    }
}