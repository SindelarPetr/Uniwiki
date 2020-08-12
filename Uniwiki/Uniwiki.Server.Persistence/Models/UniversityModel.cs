using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class UniversityModel : RemovableModelBase<Guid>
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

        internal UniversityModel(Guid id, string fullName, string shortName, string url, bool isRemoved) : base(isRemoved, id)
        {
            FullName = fullName;
            ShortName = shortName;
            Url = url;
        }

        protected UniversityModel()
        {

        }
    }
}