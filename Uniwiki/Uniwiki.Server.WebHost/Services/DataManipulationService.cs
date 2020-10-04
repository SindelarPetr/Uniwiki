using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared;
using Shared.Services;
using Shared.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.WebHost.Services
{
    public class DataManipulationService
    {
        private readonly UniwikiContext _uniwikiContext;
        private readonly StringStandardizationService _stringStandardizationService;

        public DataManipulationService(UniwikiContext uniwikiContext, StringStandardizationService stringStandardizationService)
        {
            _uniwikiContext = uniwikiContext;
            _stringStandardizationService = stringStandardizationService;
        }

        public async Task InitData()
        {
            _uniwikiContext.Database.EnsureDeleted();
            _uniwikiContext.Database.EnsureCreated();

            // ---------------- Universities ----------------
            var cvut = _uniwikiContext.Universities.Add(
                new UniversityModel(Guid.NewGuid(), "České Vysoké Učení Technické v Praze", "ČVUT", 
                    "cvut")).Entity;

            var hse = _uniwikiContext.Universities.Add(new UniversityModel(Guid.NewGuid(), "Higher School of Economics",
                "HSE", "hse")).Entity;
            var czu = _uniwikiContext.Universities.Add(new UniversityModel(Guid.NewGuid(), "Česká Zemědělská Univerzita", "ČZU", "czu")).Entity;

            _uniwikiContext.SaveChanges();

            // ---------------- Study groups ----------------

            var cvutFit = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), cvut.Id, "FIT", "Fakulta Informačních Technologií", "fit", Language.Czech)).Entity;
            var cvutFel = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), cvut.Id, "FEL", "Fakulta Elektro Technická", "fel", Language.Czech)).Entity;
            var cvutFs = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), cvut.Id, "FS", "Fakulta Strojní", "fs", Language.Czech)).Entity;
            var cvutFa = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), cvut.Id, "FA", "Fakulta Architektury", "fa", Language.Czech)).Entity;
            var cvutFsv = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), cvut.Id, "FSv", "Fakulta Stavební", "fsv", Language.Czech)).Entity;
            var hseWe = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), hse.Id, "WE", "Faculty of World Economy and International Affairs", "we", Language.English)).Entity;
            var hseLaw = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), hse.Id, "FL", "Faculty of Law", "fl", Language.English)).Entity;
            var czuPef = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), czu.Id, "PEF", "Provozně ekonomická Fakulta", "pef", Language.Czech)).Entity;
            var czuFappz = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), czu.Id, "FAPPZ", "Fakulta agrobiologie, potravinových a přírodních zdrojů", "fappz", Language.Czech)).Entity;
            var czuTf = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), czu.Id, "TF", "Technická fakulta", "tf", Language.Czech)).Entity;

            _uniwikiContext.SaveChanges();

            // ---------------- Courses ----------------
            Guid aId = Guid.NewGuid();
            // CVUT FIT
            CreateCourse("BI-3D", "3D Tisk", cvutFit.Id, aId, cvut.Url, "bi-3d", cvutFit.Url);
            CreateCourse("BI-EMP", "Ekonomické principy a management", cvutFit.Id, aId, cvut.Url, "bi-emp", cvutFit.Url);
            CreateCourse("BI-CS1", "Programování v C# 1", cvutFit.Id, aId, cvut.Url, "bi-cs1", cvutFit.Url);
            CreateCourse("BI-CS2", "Programování v C# 2", cvutFit.Id, aId, cvut.Url, "bi-cs2", cvutFit.Url);
            CreateCourse("BI-CS3", "Programování v C# 3", cvutFit.Id, aId, cvut.Url, "bi-cs3", cvutFit.Url);
            CreateCourse("BI-AAG", "Automaty a gramatiky", cvutFit.Id, aId, cvut.Url, "bi-aag", cvutFit.Url);
            CreateCourse("BI-AG1", "Algoritmy a grafy 1", cvutFit.Id, aId, cvut.Url, "bi-ag1", cvutFit.Url);
            CreateCourse("BI-BEZ", "Bezpečnost", cvutFit.Id, aId, cvut.Url, "bi-bez", cvutFit.Url);
            CreateCourse("BI-CAO", "Číslicové a analogové obvody", cvutFit.Id, aId, cvut.Url, "bi-cao", cvutFit.Url);
            CreateCourse("BI-DBS", "Databázové systémy", cvutFit.Id, aId, cvut.Url, "bi-dbs", cvutFit.Url);
            CreateCourse("BI-MLO", "Matematická logika", cvutFit.Id, aId, cvut.Url, "bi-mlo", cvutFit.Url);
            CreateCourse("BI-PA1", "Programování a algoritmizace 1", cvutFit.Id, aId, cvut.Url, "bi-pa1", cvutFit.Url);
            CreateCourse("BI-PA2", "Programování a algoritmizace 2", cvutFit.Id, aId, cvut.Url, "bi-pa2", cvutFit.Url);
            CreateCourse("BI-OSY", "Operační systémy", cvutFit.Id, aId, cvut.Url, "bi-osy", cvutFit.Url);
            var cvutFitLinId = CreateCourse("BI-LIN", "Lineární algebra", cvutFit.Id, aId, cvut.Url, "bi-lin", cvutFit.Url);

            // HSE FE courses
            CreateCourse("", "Economics of Natural Resources", hseWe.Id, aId, hse.Url, "economics-of-natural-sciences", hseWe.Url);
            CreateCourse("", "Game Theory", hseWe.Id, aId, hse.Url, "game-theory", hseWe.Url);
            CreateCourse("", "Digital Transformation of the World Economy", hseWe.Id, aId, hse.Url, "digital-transformation-of-the-world-economy", hseWe.Url);
            CreateCourse("", "Mergers, Acquisitions and Restructuring of a Firm", hseWe.Id, aId, hse.Url, "mergers-acquisitions-and-restructuring-or-a-firm", hseWe.Url);
            CreateCourse("", "Mergers and Acquisitions in Financial Markets", hseWe.Id, aId, hse.Url, "economics-of-natural-sciences", hseWe.Url);
            CreateCourse("", "Microeconomic Methods of Economic Policy Analysis", hseWe.Id, aId, hse.Url, "microeconomics-methods-of-economic-policy-analysis", hseWe.Url);
            CreateCourse("", "Models with Qualitative Dependent Variables", hseWe.Id, aId, hse.Url, "models-with-qualitative-dependent-variables", hseWe.Url);
            CreateCourse("", "Microeconomics: applications", hseWe.Id, aId, hse.Url, "microeconomics-applications", hseWe.Url);
            CreateCourse("", "Microeconomics", hseWe.Id, aId, hse.Url, "microeconomics", hseWe.Url);
            CreateCourse("", "Personnel Economics", hseWe.Id, aId, hse.Url, "personnel-economics", hseWe.Url);
            CreateCourse("", "Personal Money Management", hseWe.Id, aId, hse.Url, "personal-money-management", hseWe.Url);
            CreateCourse("", "Portfolio Management", hseWe.Id, aId, hse.Url, "portfolio-management", hseWe.Url);
            CreateCourse("", "Principles of Corporate Finance", hseWe.Id, aId, hse.Url, "principles-of-corporate-finance", hseWe.Url);
            CreateCourse("", "Econometrics of Program Evaluation", hseWe.Id, aId, hse.Url, "econometrics-of-program-evaluation", hseWe.Url);
            CreateCourse("", "Empirical Industrial Organisations", hseWe.Id, aId, hse.Url, "empirical-industrial-organizations", hseWe.Url);
            CreateCourse("", "English for Financiers (Advanced Level)", hseWe.Id, aId, hse.Url, "english-for-financiers-advanced-level", hseWe.Url);
            CreateCourse("", "Financial Innovation", hseWe.Id, aId, hse.Url, "financial-innovation", hseWe.Url);
            CreateCourse("", "Financial Markets: Problems and Decisions", hseWe.Id, aId, hse.Url, "financial-markets-problems-and-decisions", hseWe.Url);
            CreateCourse("", "Fundamental and Technical Analysis", hseWe.Id, aId, hse.Url, "fundamental-and-technical-analysis", hseWe.Url);
            CreateCourse("", "Advanced Microeconomics", hseWe.Id, aId, hse.Url, "advanced-microeconomics", hseWe.Url);
            CreateCourse("", "Behavioral Finance", hseWe.Id, aId, hse.Url, "behavioral-finance", hseWe.Url);
            CreateCourse("", "Effective economics", hseWe.Id, aId, hse.Url, "effective-economics", hseWe.Url);

            _uniwikiContext.SaveChanges();
        }

        private Guid CreateCourse(string? code, string fullName, Guid studyGroupId, Guid authorId, string universityUrl, string url, string studyGroupUrl)
        {
            var course = new CourseModel(
                Guid.NewGuid(),
                code,
                code == null ? null : _stringStandardizationService.StandardizeSearchText(code),
                fullName,
                _stringStandardizationService.StandardizeSearchText(fullName),
                authorId,
                studyGroupId,
                universityUrl,
                url,
                studyGroupUrl);

            _uniwikiContext.Add(course);

            return course.Id;
        }
    }
}
