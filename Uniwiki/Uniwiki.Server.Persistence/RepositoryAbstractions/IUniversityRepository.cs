﻿using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface IUniversityRepository : IIdRepository<UniversityModel, Guid>, IRemovableRepository<UniversityModel>
    {
        UniversityModel GetUniversityByUrlName(string universityUrlName);
        IEnumerable<UniversityModel> FindUniversities(string text);
        IEnumerable<UniversityModel> GetUniversities();
        IEnumerable<UniversityModel> SearchUniversities(string text);
        bool IsNameAndUrlUniq(string fullName, string url);
    }
}