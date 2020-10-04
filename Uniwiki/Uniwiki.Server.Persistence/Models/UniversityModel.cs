using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Uniwiki.Server.Persistence.Models.Base;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Shared;

namespace Uniwiki.Server.Persistence.Models
{
    public class UniversityModel : ModelBase<Guid>
    {
        /// <summary>
        /// e. g. 'IT University of Copenhagen'
        /// </summary>
        [MaxLength(Constants.Validations.UniversityLongName)]
        public string LongName { get; set; } = null!;

        /// <summary>
        /// e. g. 'ITU'
        /// </summary>
        [MaxLength(Constants.Validations.UniversityShortName)]
        public string ShortName { get; set; } = null!;

        [MaxLength(Constants.Validations.UrlMaxLength)]
        public string Url { get; set; } = null!;

        public ICollection<StudyGroupModel> StudyGroups { get; set; } = null!;

        public UniversityModel(Guid id, string fullName, string shortName, string url) : base(id)
        {
            LongName = fullName;
            ShortName = shortName;
            Url = url;
            StudyGroups = new List<StudyGroupModel>();
        }

        protected UniversityModel()
        {

        }
    }
}