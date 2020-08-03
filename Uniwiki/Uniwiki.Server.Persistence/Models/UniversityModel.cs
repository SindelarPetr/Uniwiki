using System;
using System.Collections.Generic;
using Shared.Extensions;

namespace Uniwiki.Server.Persistence.Models
{
    public class UniversityModel
    {
        public Guid Id { get; protected set; }

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

        internal UniversityModel()
        {

        }

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