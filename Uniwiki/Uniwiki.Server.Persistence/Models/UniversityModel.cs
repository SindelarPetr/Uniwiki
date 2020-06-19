using System;
using System.Collections.Generic;
using Shared.Extensions;

namespace Uniwiki.Server.Persistence.Models
{
    public class UniversityModel
    {
        public Guid Id { get; }

        /// <summary>
        /// e. g. 'IT University of Copenhagen'
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// e. g. 'ITU'
        /// </summary>
        public string ShortName { get; }

        public string Url
        { get; }

        public IEnumerable<StudyGroupModel> StudyGroups { get; private set; }

        internal UniversityModel(Guid id, string fullName, string shortName, string url, IEnumerable<StudyGroupModel> studyGroups)
        {
            Id = id;
            FullName = fullName;
            ShortName = shortName;
            StudyGroups = studyGroups;
            Url = url;
        }
    }
}