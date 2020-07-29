using Shared.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Shared.Services
{
    public class FileHelperService : IFileHelperService
    {
        public (string fileName, string extension) GetFileNameAndExtension(string fullFileName)
        {
            var dotsCount = fullFileName.Count(ch => ch == '.');
            if (dotsCount == 1 && fullFileName.First() == '.')
                return (fullFileName, string.Empty);

            var extension = Path.GetExtension(fullFileName);
            var fileName = Path.GetFileNameWithoutExtension(fullFileName);

            return (fileName, extension);
        }
    }
}
