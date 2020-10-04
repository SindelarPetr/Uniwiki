using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uniwiki.Server.WebHost.ViewModels
{
    public class UniversityViewModel
    {
        public string ShortName { get; }
        public string LongName { get; }
        public FacultyItemViewModel[] FacultyItems { get; }

        public UniversityViewModel(string shortName,
            string longName,
            FacultyItemViewModel[] facultyItems)
        {
            ShortName = shortName;
            LongName = longName;
            FacultyItems = facultyItems;
        }
    }

    public class FacultyItemViewModel
    {
        public FacultyItemViewModel(string shortName, string longName, string url)
        {
            ShortName = shortName;
            LongName = longName;
            Url = url;
        }

        public string ShortName { get; }
        public string LongName { get; }
        public string Url { get; }
    }
}
