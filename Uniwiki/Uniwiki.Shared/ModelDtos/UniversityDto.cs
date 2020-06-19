using System;

namespace Uniwiki.Shared.ModelDtos
{
    public class UniversityDto
    {
        public UniversityDto(Guid id, string longName, string shortName, string url)
        {
            Id = id;
            LongName = longName;
            ShortName = shortName;
            Url = url;
        }

        public Guid Id { get; set; }

        /// <summary>
        /// e. g. 'IT University of Copenhagen'
        /// </summary>
        public string LongName { get; set; }

        /// <summary>
        /// e. g. 'ITU'
        /// </summary>
        public string ShortName { get; set; }

        public string Url { get; set; }
    }
}