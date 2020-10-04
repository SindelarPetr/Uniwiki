using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Server.Appliaction.ServerActions;
using Shared;
using Shared.Services;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.ServerActions;
using Uniwiki.Server.Application.ServerActions.Authentication;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.Services
{


    public class DataManipulationService
    {
        private readonly UniwikiContext _uniwikiContext;
        private readonly IHashService _hashService;
        private readonly IServiceProvider _serviceProvider;
        private readonly StringStandardizationService _stringStandardizationService;
        private readonly AddPostCommentServerAction _addPostCommentServerAction;
        private readonly LikePostCommentServerAction _likePostCommentServerAction;
        private readonly LikePostServerAction _likePostServerAction;
        private readonly AddPostServerAction _addPostServerAction;
        private readonly ProfileRepository _profileRepository;
        private readonly ITimeService _timeService;
        private readonly IEmailService _emailService;
        private readonly RequestContext _anonymousContext = new RequestContext(null, AuthenticationLevel.None, Guid.NewGuid().ToString(), Language.English, new System.Net.IPAddress(0x2414188));

        //public DataManipulationService(IServiceProvider serviceProvider, AddPostCommentServerAction addCommentServerAction, LikePostCommentServerAction likePostCommentServerAction, LikePostServerAction likePostServerAction, AddPostServerAction addPostServerAction, ProfileRepository profileRepository, ITimeService timeService, IEmailService emailService)
        //{
        //    _serviceProvider = serviceProvider;
        //    _addPostCommentServerAction = addCommentServerAction;
        //    _likePostCommentServerAction = likePostCommentServerAction;
        //    _likePostServerAction = likePostServerAction;
        //    _addPostServerAction = addPostServerAction;
        //    _profileRepository = profileRepository;
        //    _timeService = timeService;
        //    _emailService = emailService;
        //}

        public DataManipulationService(UniwikiContext uniwikiContext, IHashService hashService, IServiceProvider serviceProvider, StringStandardizationService stringStandardizationService)
        {
            _uniwikiContext = uniwikiContext;
            _hashService = hashService;
            _serviceProvider = serviceProvider;
            _stringStandardizationService = stringStandardizationService;
        }

        private Guid RegisterUser(string email, string firstName, string familyName, string url, string password, bool isAdmin)
        {
            var id = Guid.NewGuid();

            var passwordHash = _hashService.HashPassword(password);

            _uniwikiContext.Profiles.Add(new ProfileModel(
                id, 
                email, 
                firstName, 
                familyName, 
                url, 
                passwordHash.hashedPassword, 
                passwordHash.salt,
                $"/img/profilePictures/no-profile-picture.jpg",
                DateTime.Now, 
                true,
                isAdmin ? AuthenticationLevel.Admin : AuthenticationLevel.PrimaryToken,
                null));
            return id;
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

        /// <summary>
        /// Initializes the fake data.
        /// </summary>
        /// <returns></returns>
         public async Task InitializeFakeData()
        {
             return; // Comment this line to delete and re-create the database
            _uniwikiContext.Database.EnsureDeleted();
            _uniwikiContext.Database.EnsureCreated();

            // Create users
            var aId = RegisterUser("a@a.cz", "Admin", "Novak", "admin-novak", "a", true);
            var bId = RegisterUser("b@b.cz", "Barbora", "Zelená", "barbora-zelena", "b", false);
            var cId = RegisterUser("c@c.cz", "Martin", "Modry", "martin-modry", "c", false);

            _uniwikiContext.SaveChanges();

            // Create universities
            var cvut = _uniwikiContext.Universities.Add(new UniversityModel(Guid.NewGuid(), "České Vysoké Učení Technické", "ČVUT", "cvut"));
            var hse = _uniwikiContext.Universities.Add(new UniversityModel(Guid.NewGuid(), "Higher School Of Economics", "HSE", "hse"));
            var czu = _uniwikiContext.Universities.Add(new UniversityModel(Guid.NewGuid(), "Česká Zemědělská Univerzita", "ČZU", "czu"));

            _uniwikiContext.SaveChanges();

            // Create faculties
            var cvutFit = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), cvut.Entity.Id, "FIT", "Fakulta Informačních Technologií", "fit", Language.Czech));
            var cvutFel = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), cvut.Entity.Id, "FEL", "Fakulta Elektro Technická", "fel", Language.Czech));
            var cvutFs = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), cvut.Entity.Id, "FS", "Fakulta Strojní", "fs", Language.Czech));
            var cvutFa = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), cvut.Entity.Id, "FA", "Fakulta Architektury", "fa", Language.Czech));
            var cvutFsv = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), cvut.Entity.Id, "FSv", "Fakulta Stavební", "fsv", Language.Czech));
            var hseWe = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), hse.Entity.Id, "WE", "Faculty of World Economy and International Affairs", "we", Language.English));
            var hseLaw = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), hse.Entity.Id, "FL", "Faculty of Law", "fl", Language.English));
            var czuPef = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), czu.Entity.Id, "PEF", "Provozně ekonomická Fakulta", "pef", Language.Czech));
            var czuFappz = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), czu.Entity.Id, "FAPPZ", "Fakulta agrobiologie, potravinových a přírodních zdrojů", "fappz", Language.Czech));
            var czuTf = _uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(), czu.Entity.Id, "TF", "Technická fakulta", "tf", Language.Czech));

            _uniwikiContext.SaveChanges();

            // Courses
            // CVUT FIT
            CreateCourse("BI-3D", "3D Tisk", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-3d", cvutFit.Entity.Url);
            CreateCourse("BI-EMP", "Ekonomické principy a management", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-emp", cvutFit.Entity.Url);
            CreateCourse("BI-CS1", "Programování v C# 1", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-cs1", cvutFit.Entity.Url);
            CreateCourse("BI-CS2", "Programování v C# 2", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-cs2", cvutFit.Entity.Url);
            CreateCourse("BI-CS3", "Programování v C# 3", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-cs3", cvutFit.Entity.Url);
            CreateCourse("BI-AAG", "Automaty a gramatiky", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-aag", cvutFit.Entity.Url);
            CreateCourse("BI-AG1", "Algoritmy a grafy 1", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-ag1", cvutFit.Entity.Url);
            CreateCourse("BI-BEZ", "Bezpečnost", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-bez", cvutFit.Entity.Url);
            CreateCourse("BI-CAO", "Číslicové a analogové obvody", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-cao", cvutFit.Entity.Url);
            CreateCourse("BI-DBS", "Databázové systémy", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-dbs", cvutFit.Entity.Url);
            CreateCourse("BI-MLO", "Matematická logika", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-mlo", cvutFit.Entity.Url);
            CreateCourse("BI-PA1", "Programování a algoritmizace 1", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-pa1", cvutFit.Entity.Url);
            CreateCourse("BI-PA2", "Programování a algoritmizace 2", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-pa2", cvutFit.Entity.Url);
            CreateCourse("BI-OSY", "Operační systémy", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-osy", cvutFit.Entity.Url);
            var cvutFitLinId = CreateCourse("BI-LIN", "Lineární algebra", cvutFit.Entity.Id, aId, cvut.Entity.Url, "bi-lin", cvutFit.Entity.Url);

            // HSE FE courses
            CreateCourse("", "Economics of Natural Resources", hseWe.Entity.Id, aId, hse.Entity.Url, "economics-of-natural-sciences", hseWe.Entity.Url);
            CreateCourse("", "Game Theory", hseWe.Entity.Id, aId, hse.Entity.Url, "game-theory", hseWe.Entity.Url);
            CreateCourse("", "Digital Transformation of the World Economy", hseWe.Entity.Id, aId, hse.Entity.Url, "digital-transformation-of-the-world-economy", hseWe.Entity.Url);
            CreateCourse("", "Mergers, Acquisitions and Restructuring of a Firm", hseWe.Entity.Id, aId, hse.Entity.Url, "mergers-acquisitions-and-restructuring-or-a-firm", hseWe.Entity.Url);
            CreateCourse("", "Mergers and Acquisitions in Financial Markets", hseWe.Entity.Id, aId, hse.Entity.Url, "economics-of-natural-sciences", hseWe.Entity.Url);
            CreateCourse("", "Microeconomic Methods of Economic Policy Analysis", hseWe.Entity.Id, aId, hse.Entity.Url, "microeconomics-methods-of-economic-policy-analysis", hseWe.Entity.Url);
            CreateCourse("", "Models with Qualitative Dependent Variables", hseWe.Entity.Id, aId, hse.Entity.Url, "models-with-qualitative-dependent-variables", hseWe.Entity.Url);
            CreateCourse("", "Microeconomics: applications", hseWe.Entity.Id, aId, hse.Entity.Url, "microeconomics-applications", hseWe.Entity.Url);
            CreateCourse("", "Microeconomics", hseWe.Entity.Id, aId, hse.Entity.Url, "microeconomics", hseWe.Entity.Url);
            CreateCourse("", "Personnel Economics", hseWe.Entity.Id, aId, hse.Entity.Url, "personnel-economics", hseWe.Entity.Url);
            CreateCourse("", "Personal Money Management", hseWe.Entity.Id, aId, hse.Entity.Url, "personal-money-management", hseWe.Entity.Url);
            CreateCourse("", "Portfolio Management", hseWe.Entity.Id, aId, hse.Entity.Url, "portfolio-management", hseWe.Entity.Url);
            CreateCourse("", "Principles of Corporate Finance", hseWe.Entity.Id, aId, hse.Entity.Url, "principles-of-corporate-finance", hseWe.Entity.Url);
            CreateCourse("", "Econometrics of Program Evaluation", hseWe.Entity.Id, aId, hse.Entity.Url, "econometrics-of-program-evaluation", hseWe.Entity.Url);
            CreateCourse("", "Empirical Industrial Organisations", hseWe.Entity.Id, aId, hse.Entity.Url, "empirical-industrial-organizations", hseWe.Entity.Url);
            CreateCourse("", "English for Financiers (Advanced Level)", hseWe.Entity.Id, aId, hse.Entity.Url, "english-for-financiers-advanced-level", hseWe.Entity.Url);
            CreateCourse("", "Financial Innovation", hseWe.Entity.Id, aId, hse.Entity.Url, "financial-innovation", hseWe.Entity.Url);
            CreateCourse("", "Financial Markets: Problems and Decisions", hseWe.Entity.Id, aId, hse.Entity.Url, "financial-markets-problems-and-decisions", hseWe.Entity.Url);
            CreateCourse("", "Fundamental and Technical Analysis", hseWe.Entity.Id, aId, hse.Entity.Url, "fundamental-and-technical-analysis", hseWe.Entity.Url);
            CreateCourse("", "Advanced Microeconomics", hseWe.Entity.Id, aId, hse.Entity.Url, "advanced-microeconomics", hseWe.Entity.Url);
            CreateCourse("", "Behavioral Finance", hseWe.Entity.Id, aId, hse.Entity.Url, "behavioral-finance", hseWe.Entity.Url);
            CreateCourse("", "Effective economics", hseWe.Entity.Id, aId, hse.Entity.Url, "effective-economics", hseWe.Entity.Url);

            _uniwikiContext.SaveChanges();

            // Posts
            var postOne = _uniwikiContext.Posts.Add(new PostModel(Guid.NewGuid(), null, aId, "This is some interesting post 1!", cvutFitLinId, DateTime.Now));
            var postOneAgain = _uniwikiContext.Posts.Add(new PostModel(Guid.NewGuid(), null, aId, "This is some interesting post 1 again!", cvutFitLinId, DateTime.Now));
            var postTwo = _uniwikiContext.Posts.Add(new PostModel(Guid.NewGuid(), null, bId, "This is some interesting post 2!", cvutFitLinId, DateTime.Now));
            var postThree = _uniwikiContext.Posts.Add(new PostModel(Guid.NewGuid(), null, cId, "This is some interesting post 3!", cvutFitLinId, DateTime.Now));

            _uniwikiContext.SaveChanges();

            // Post files
            _uniwikiContext.PostFiles.Add(new PostFileModel(Guid.NewGuid(), "some/file/",
                "Exams and some others", "pdf", true, aId, cvutFitLinId, DateTime.Now, 30000){PostId = postOne.Entity.Id});

            _uniwikiContext.SaveChanges();

            // Comments
            var commentOne = _uniwikiContext.PostComments.Add(new PostCommentModel(Guid.NewGuid(), aId, postOne.Entity.Id, "I dont beleive that.", DateTime.Now));
            var commentOneAgain = _uniwikiContext.PostComments.Add(new PostCommentModel(Guid.NewGuid(), aId, postOne.Entity.Id, "I dont beleive that again.", DateTime.Now));
            var commentTwo = _uniwikiContext.PostComments.Add(new PostCommentModel(Guid.NewGuid(), bId, postOne.Entity.Id, "I dont beleive that as well.", DateTime.Now));
            var commentTwoAgain = _uniwikiContext.PostComments.Add(new PostCommentModel(Guid.NewGuid(), bId, postOne.Entity.Id, "I dont beleive that as well again.", DateTime.Now));

            _uniwikiContext.SaveChanges();

            // Post Likes

            _uniwikiContext.PostLikes.Add(new PostLikeModel(postOne.Entity.Id, aId, DateTime.Now, true));
            _uniwikiContext.PostLikes.Add(new PostLikeModel(postOne.Entity.Id, bId, DateTime.Now, true));
            _uniwikiContext.PostLikes.Add(new PostLikeModel(postOne.Entity.Id, cId, DateTime.Now, true));
            _uniwikiContext.PostLikes.Add(new PostLikeModel(postTwo.Entity.Id, cId, DateTime.Now, true));

            _uniwikiContext.SaveChanges();

            // Comment Likes
            _uniwikiContext.PostCommentLikes.Add(new PostCommentLikeModel(commentOne.Entity.Id, aId, DateTime.Now, true));
            _uniwikiContext.PostCommentLikes.Add(new PostCommentLikeModel(commentOne.Entity.Id, bId, DateTime.Now, false));
            _uniwikiContext.PostCommentLikes.Add(new PostCommentLikeModel(commentOne.Entity.Id, cId, DateTime.Now, true));
            _uniwikiContext.PostCommentLikes.Add(new PostCommentLikeModel(commentTwo.Entity.Id, cId, DateTime.Now, true));

            _uniwikiContext.SaveChanges();

            //// HSE ECO
            //await CreateCourse("", "Economic geography", hseWe.Entity.Id, aId, hse.Entity.Url, "economics-of-natural-sciences", hseEco.Entity.Url);

            //// HSE FCS
            //await CreateCourse("", "Effective economics", fcsHse, lucieContext);

            //// HSE FM
            //await CreateCourse("", "Effective economics", fmHse, lucieContext);

            //// HSE FSS
            //await CreateCourse("", "Effective economics", fssHse, lucieContext);

            //// HSE FH
            //await CreateCourse("", "Effective economics", fhHse, lucieContext);

            //// HSE FBM
            //await CreateCourse("", "Effective economics", fbmHse, lucieContext);

            //// HSE Law
            //await CreateCourse("", "Administrative Law", lawHse, lucieContext);
            //await CreateCourse("", "Academic English Writing", lawHse, lucieContext);
            //await CreateCourse("", "English Contract Law", lawHse, lucieContext);
            //await CreateCourse("", "Commercial Procedure", lawHse, lucieContext);
            //await CreateCourse("", "Bankruptcy Businesses", lawHse, lucieContext);
            //await CreateCourse("", "Safe Living Basics", lawHse, lucieContext);
            //await CreateCourse("", "An Introduction to American Law", lawHse, lucieContext);
            //await CreateCourse("", "Civil Law", lawHse, lucieContext);
            //await CreateCourse("", "Land Law", lawHse, lucieContext);
            //await CreateCourse("", "Internet Giants: The Law and Economics of Media Platforms", lawHse, lucieContext);
            //await CreateCourse("", "Information Law", lawHse, lucieContext);
            //await CreateCourse("", "Information Technologies in the Activity of Lawyer", lawHse, lucieContext);
            //await CreateCourse("", "History of Political and Legal Doctrines", lawHse, lucieContext);
            //await CreateCourse("", "Competition Law", lawHse, lucieContext);
            //await CreateCourse("", "Constitutional Law of Foreign Countries", lawHse, lucieContext);
            //await CreateCourse("", "Constitutional Challenges in the Islamic World", lawHse, lucieContext);
            //await CreateCourse("", "Forensic Science", lawHse, lucieContext);
            //await CreateCourse("", "Criminology", lawHse, lucieContext);
            //var hseLawFmId = (await CreateCourse("", "Financial Markets", lawHse, lucieContext)).CourseId;
            //var hseLawM2Id = (await CreateCourse("", "Advanced Management", lawHse, lucieContext)).CourseId;
            //await CreateCourse("", "International Law in Action: the Arbitration of International Disputes", lawHse, lucieContext);
            //await CreateCourse("", "International Law in Action: A Guide to the International Courts and Tribunals in The Hague", lawHse, lucieContext);


            //// HSE WE
            //await CreateCourse("", "Academic English Writing", weHse, lucieContext);
            //await CreateCourse("", "Introduction to International Affairs and World Economy", weHse, lucieContext);
            //await CreateCourse("", "Global Challenges and Issues", weHse, lucieContext);
            //await CreateCourse("", "Foreign Language (English for Professional Purposes)", weHse, lucieContext);
            //await CreateCourse("", "Foreign Language (Arabic)", weHse, lucieContext);
            //await CreateCourse("", "International Culture", weHse, lucieContext);
            //await CreateCourse("", "Research Seminar \"Key Tendencies of the Modern Middle East and Northern Africa Development\"", weHse, lucieContext);
            //await CreateCourse("", "Research Seminar \"Methods in International Studies\"", weHse, lucieContext);
            //await CreateCourse("", "Scientific Seminar The Euro Atlantic Region: Economic and Political Problems", weHse, lucieContext);
            //await CreateCourse("", "National and International Security", weHse, lucieContext);
            //await CreateCourse("", "National and Multilateral Governance", weHse, lucieContext);
            //await CreateCourse("", "Key development tendencies of Eurasia and Trans-Pacific Region", weHse, lucieContext);
            //await CreateCourse("", "Introduction to International Business", weHse, lucieContext);
            //await CreateCourse("", "Political history of the Middle East", weHse, lucieContext);
            //await CreateCourse("", "Political History of China", weHse, lucieContext);
            //await CreateCourse("", "Political History of USA", weHse, lucieContext);
            //await CreateCourse("", "Law", weHse, lucieContext);
            //await CreateCourse("", "Economy and Politics of Arab Countries", weHse, lucieContext);
            //await CreateCourse("", "Energy policy and Diplomacy", weHse, lucieContext);
            //var hseWeFmId = (await CreateCourse("", "Financial Markets", weHse, lucieContext)).CourseId;
            //var hseWeM2Id = (await CreateCourse("", "Advanced Management", weHse, lucieContext)).CourseId;

            //// Make sure no emails will be sent
            //_emailService.DisableSendingEmails();

            //// Register users
            //var lucieContext = await RegisterUser("a@a.cz", "Lucie", "Veselá", "aaaaaa", true);
            //var ivanaContext = await RegisterUser("b@b.cz", "Ivana", "Nováková", "aaaaaa");
            //var terezieContext = await RegisterUser("c@c.cz", "Marie", "Terezie", "aaaaaa");
            //var petrContext = await RegisterUser("d@d.cz", "Petr", "Šindelář", "aaaaaa");
            //var barboraContext = await RegisterUser("e@e.cz", "Barbora", "Zelená", "aaaaaa");
            //var martinContext = await RegisterUser("f@f.cz", "Martin", "Novák", "aaaaaa");

            //// Universities
            //var uniCvut = (await Scoped<AddUniversityServerAction>().ExecuteActionAsync(new AddUniversityRequestDto("České vysoké učení technické v Praze", "ČVUT", "cvut"), lucieContext)).University;
            //var uniVse = (await Scoped<AddUniversityServerAction>().ExecuteActionAsync(new AddUniversityRequestDto("Vysoká škola ekonomická", "VŠE", "vse"), lucieContext)).University;
            //var uniCzu = (await Scoped<AddUniversityServerAction>().ExecuteActionAsync(new AddUniversityRequestDto("Česká zemědělská universita", "ČZU", "czu"), lucieContext)).University;
            //var uniHse = (await Scoped<AddUniversityServerAction>().ExecuteActionAsync(new AddUniversityRequestDto("Higher school of economics", "HSE", "hse"), lucieContext)).University;
            //var uniUof = (await Scoped<AddUniversityServerAction>().ExecuteActionAsync(new AddUniversityRequestDto("University of Oxford", "UOF", "uof"), lucieContext)).University;

            //// Study groups (faculties)
            //var fitCvut = (await Scoped<AddStudyGroupServerAction>().ExecuteActionAsync(
            //    new AddStudyGroupRequestDto("Fakulta informačních technologií", "FIT", uniCvut.Id, Language.Czech), lucieContext)).StudyGroupId;
            //var ecoHse = (await Scoped<AddStudyGroupServerAction>().ExecuteActionAsync(
            //    new AddStudyGroupRequestDto("Faculty of Economic Sciences", "FES", uniHse.Id, Language.English), lucieContext)).StudyGroupId;
            //var lawHse = (await Scoped<AddStudyGroupServerAction>().ExecuteActionAsync(
            //    new AddStudyGroupRequestDto("Faculty of Law", "FL", uniHse.Id, Language.English), lucieContext)).StudyGroupId;
            //var weHse = (await Scoped<AddStudyGroupServerAction>().ExecuteActionAsync(
            //    new AddStudyGroupRequestDto("Faculty of World Economy and International Affairs", "WE", uniHse.Id, Language.English), lucieContext)).StudyGroupId;
            //var fcsHse = (await Scoped<AddStudyGroupServerAction>().ExecuteActionAsync(
            //    new AddStudyGroupRequestDto("Faculty of Computer Science", "FCS", uniHse.Id, Language.English), lucieContext)).StudyGroupId;
            //var fmHse = (await Scoped<AddStudyGroupServerAction>().ExecuteActionAsync(
            //    new AddStudyGroupRequestDto("Faculty of Mathematics", "FM", uniHse.Id, Language.English), lucieContext)).StudyGroupId;
            //var fssHse = (await Scoped<AddStudyGroupServerAction>().ExecuteActionAsync(
            //    new AddStudyGroupRequestDto("Faculty of Social Sciences", "FSS", uniHse.Id, Language.English), lucieContext)).StudyGroupId;
            //var fhHse = (await Scoped<AddStudyGroupServerAction>().ExecuteActionAsync(
            //    new AddStudyGroupRequestDto("Faculty of Humanities", "FH", uniHse.Id, Language.English), lucieContext)).StudyGroupId;
            //var fbmHse = (await Scoped<AddStudyGroupServerAction>().ExecuteActionAsync(
            //    new AddStudyGroupRequestDto("Faculty of Business and Management", "FBM", uniHse.Id, Language.English), lucieContext)).StudyGroupId;

            //// Courses

            //// CVUT FIT
            //await CreateCourse("BI-3D", "3D Tisk", fitCvut, lucieContext);
            //await CreateCourse("BI-EMP", "Ekonomické principy a management", fitCvut, lucieContext);
            //await CreateCourse("BI-CS1", "Programování v C# 1", fitCvut, lucieContext);
            //await CreateCourse("BI-CS2", "Programování v C# 2", fitCvut, lucieContext);
            //await CreateCourse("BI-CS3", "Programování v C# 3", fitCvut, lucieContext);
            //await CreateCourse("BI-AAG", "Automaty a gramatiky", fitCvut, lucieContext);
            //await CreateCourse("BI-AG1", "Algoritmy a grafy 1", fitCvut, lucieContext);
            //await CreateCourse("BI-BEZ", "Bezpečnost", fitCvut, lucieContext);
            //await CreateCourse("BI-CAO", "Číslicové a analogové obvody", fitCvut, lucieContext);
            //await CreateCourse("BI-DBS", "Databázové systémy", fitCvut, lucieContext);
            //await CreateCourse("BI-MLO", "Matematická logika", fitCvut, lucieContext);
            //await CreateCourse("BI-PA1", "Programování a algoritmizace 1", fitCvut, lucieContext);
            //await CreateCourse("BI-PA2", "Programování a algoritmizace 2", fitCvut, lucieContext);
            //await CreateCourse("BI-OSY", "Operační systémy", fitCvut, ivanaContext);
            //var cvutFitLinId = (await CreateCourse("BI-LIN", "Lineární algebra", fitCvut, lucieContext)).CourseId;

            //// HSE FE courses
            //await CreateCourse("", "Economics of Natural Resources", ecoHse, lucieContext);
            //await CreateCourse("", "Game Theory", ecoHse, lucieContext);
            //await CreateCourse("", "Digital Transformation of the World Economy", ecoHse, ivanaContext);
            //await CreateCourse("", "Mergers, Acquisitions and Restructuring of a Firm", ecoHse, lucieContext);
            //await CreateCourse("", "Mergers and Acquisitions in Financial Markets", ecoHse, lucieContext);
            //await CreateCourse("", "Microeconomic Methods of Economic Policy Analysis", ecoHse, lucieContext);
            //await CreateCourse("", "Models with Qualitative Dependent Variables", ecoHse, lucieContext);
            //await CreateCourse("", "Microeconomics: applications", ecoHse, ivanaContext);
            //await CreateCourse("", "Microeconomics", ecoHse, lucieContext);
            //await CreateCourse("", "Personnel Economics", ecoHse, lucieContext);
            //await CreateCourse("", "Personal Money Management", ecoHse, lucieContext);
            //await CreateCourse("", "Portfolio Management", ecoHse, ivanaContext);
            //await CreateCourse("", "Principles of Corporate Finance", ecoHse, ivanaContext);
            //await CreateCourse("", "Econometrics of Program Evaluation", ecoHse, ivanaContext);
            //await CreateCourse("", "Empirical Industrial Organisations", ecoHse, lucieContext);
            //await CreateCourse("", "English for Financiers (Advanced Level)", ecoHse, lucieContext);
            //await CreateCourse("", "Financial Innovation", ecoHse, lucieContext);
            //await CreateCourse("", "Financial Markets:Problems and Decisions", ecoHse, lucieContext);
            //await CreateCourse("", "Fundamental and Technical Analysis", ecoHse, lucieContext);
            //await CreateCourse("", "Advanced Microeconomics", ecoHse, ivanaContext);
            //await CreateCourse("", "Behavioral Finance", ecoHse, lucieContext);
            //await CreateCourse("", "Effective economics", ecoHse, lucieContext);

            //// HSE ECO
            //await CreateCourse("", "Economic geography", ecoHse, lucieContext);

            //// HSE FCS
            //await CreateCourse("", "Effective economics", fcsHse, lucieContext);

            //// HSE FM
            //await CreateCourse("", "Effective economics", fmHse, lucieContext);

            //// HSE FSS
            //await CreateCourse("", "Effective economics", fssHse, lucieContext);

            //// HSE FH
            //await CreateCourse("", "Effective economics", fhHse, lucieContext);

            //// HSE FBM
            //await CreateCourse("", "Effective economics", fbmHse, lucieContext);

            //// HSE Law
            //await CreateCourse("", "Administrative Law", lawHse, lucieContext);
            //await CreateCourse("", "Academic English Writing", lawHse, lucieContext);
            //await CreateCourse("", "English Contract Law", lawHse, lucieContext);
            //await CreateCourse("", "Commercial Procedure", lawHse, lucieContext);
            //await CreateCourse("", "Bankruptcy Businesses", lawHse, lucieContext);
            //await CreateCourse("", "Safe Living Basics", lawHse, lucieContext);
            //await CreateCourse("", "An Introduction to American Law", lawHse, lucieContext);
            //await CreateCourse("", "Civil Law", lawHse, lucieContext);
            //await CreateCourse("", "Land Law", lawHse, lucieContext);
            //await CreateCourse("", "Internet Giants: The Law and Economics of Media Platforms", lawHse, lucieContext);
            //await CreateCourse("", "Information Law", lawHse, lucieContext);
            //await CreateCourse("", "Information Technologies in the Activity of Lawyer", lawHse, lucieContext);
            //await CreateCourse("", "History of Political and Legal Doctrines", lawHse, lucieContext);
            //await CreateCourse("", "Competition Law", lawHse, lucieContext);
            //await CreateCourse("", "Constitutional Law of Foreign Countries", lawHse, lucieContext);
            //await CreateCourse("", "Constitutional Challenges in the Islamic World", lawHse, lucieContext);
            //await CreateCourse("", "Forensic Science", lawHse, lucieContext);
            //await CreateCourse("", "Criminology", lawHse, lucieContext);
            //var hseLawFmId = (await CreateCourse("", "Financial Markets", lawHse, lucieContext)).CourseId;
            //var hseLawM2Id = (await CreateCourse("", "Advanced Management", lawHse, lucieContext)).CourseId;
            //await CreateCourse("", "International Law in Action: the Arbitration of International Disputes", lawHse, lucieContext);
            //await CreateCourse("", "International Law in Action: A Guide to the International Courts and Tribunals in The Hague", lawHse, lucieContext);


            //// HSE WE
            //await CreateCourse("", "Academic English Writing", weHse, lucieContext);
            //await CreateCourse("", "Introduction to International Affairs and World Economy", weHse, lucieContext);
            //await CreateCourse("", "Global Challenges and Issues", weHse, lucieContext);
            //await CreateCourse("", "Foreign Language (English for Professional Purposes)", weHse, lucieContext);
            //await CreateCourse("", "Foreign Language (Arabic)", weHse, lucieContext);
            //await CreateCourse("", "International Culture", weHse, lucieContext);
            //await CreateCourse("", "Research Seminar \"Key Tendencies of the Modern Middle East and Northern Africa Development\"", weHse, lucieContext);
            //await CreateCourse("", "Research Seminar \"Methods in International Studies\"", weHse, lucieContext);
            //await CreateCourse("", "Scientific Seminar The Euro Atlantic Region: Economic and Political Problems", weHse, lucieContext);
            //await CreateCourse("", "National and International Security", weHse, lucieContext);
            //await CreateCourse("", "National and Multilateral Governance", weHse, lucieContext);
            //await CreateCourse("", "Key development tendencies of Eurasia and Trans-Pacific Region", weHse, lucieContext);
            //await CreateCourse("", "Introduction to International Business", weHse, lucieContext);
            //await CreateCourse("", "Political history of the Middle East", weHse, lucieContext);
            //await CreateCourse("", "Political History of China", weHse, lucieContext);
            //await CreateCourse("", "Political History of USA", weHse, lucieContext);
            //await CreateCourse("", "Law", weHse, lucieContext);
            //await CreateCourse("", "Economy and Politics of Arab Countries", weHse, lucieContext);
            //await CreateCourse("", "Energy policy and Diplomacy", weHse, lucieContext);
            //var hseWeFmId = (await CreateCourse("", "Financial Markets", weHse, lucieContext)).CourseId;
            //var hseWeM2Id = (await CreateCourse("", "Advanced Management", weHse, lucieContext)).CourseId;


            //var postTypeExperienceEn = "Experience";
            //var postTypeExperienceCz = "Zkušenost";
            //var postTypeStudyMaterialCz = "Studijní materiál";
            //var postTypeStudyMaterialEn = "Study material";
            //var postTypeHomeworkCz = "Domácí úkol";
            //var postTypeHomeworkEn = "Homework";

            //var post1 = (await _addPostServerAction.ExecuteActionAsync(
            //    new AddPostRequestDto("This was the best course so far", postTypeExperienceCz, cvutFitLinId,
            //        new PostFileDto[0]), lucieContext)).NewPost;
            //var post2 = (await _addPostServerAction.ExecuteActionAsync(
            //    new AddPostRequestDto("The course is quite cool", postTypeStudyMaterialCz, cvutFitLinId,
            //        new PostFileDto[0]), petrContext)).NewPost;
            //var post3 = (await _addPostServerAction.ExecuteActionAsync(
            //    new AddPostRequestDto("Teachers are really nice. But the syllabus (or whatever the spelling is - I dont care) quite sucks. I think you guys should work on it more. For a person who have never studied mathematics this was wayyy to hard.", postTypeExperienceCz, cvutFitLinId,
            //        new PostFileDto[0]), ivanaContext)).NewPost;
            //await _addPostServerAction.ExecuteActionAsync(
            //    new AddPostRequestDto("Teachers are really nice. But the syllabus (or whatever the spelling is - I dont care) quite sucks. I think you guys should work on it more. For a person who have never studied mathematics this was wayyy to hard.", postTypeExperienceCz, cvutFitLinId,
            //        new PostFileDto[0]), terezieContext);
            //await _addPostServerAction.ExecuteActionAsync(
            //    new AddPostRequestDto("The classes are super cool, I enjoyed every minute.", postTypeStudyMaterialCz, cvutFitLinId,
            //        new PostFileDto[0]), barboraContext);
            //await _addPostServerAction.ExecuteActionAsync(
            //    new AddPostRequestDto("This is the homework for the 3rd class, really good experience btw. I enjoyed it so much.", postTypeExperienceCz, cvutFitLinId,
            //        new PostFileDto[0]), martinContext);
            //await _addPostServerAction.ExecuteActionAsync(
            //    new AddPostRequestDto("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer convallis nibh interdum feugiat imperdiet. Phasellus bibendum tortor sed lectus maximus, in hendrerit mauris volutpat. Cras eu quam nibh. Nulla convallis commodo turpis, tempor pulvinar tellus vestibulum eu. Donec vel elit non sapien lacinia ultricies. Fusce sodales quam arcu, condimentum sodales neque posuere et. Cras pulvinar consectetur est. Curabitur aliquam nisl ipsum, vitae bibendum justo facilisis in. Donec sed eleifend turpis, a iaculis arcu. Etiam tincidunt in eros et efficitur. Quisque egestas quis magna nec egestas. Nullam interdum mi sem, accumsan dapibus magna eleifend nec. Nam viverra, lectus in pellentesque tempor, magna purus tempor enim, non facilisis quam nisi at velit. Aenean molestie mauris ac dictum suscipit.", postTypeExperienceCz, cvutFitLinId,
            //        new PostFileDto[0]), martinContext);
            //await _addPostServerAction.ExecuteActionAsync(
            //    new AddPostRequestDto("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer convallis nibh interdum feugiat imperdiet. Phasellus bibendum tortor sed lectus maximus, in hendrerit mauris volutpat. Cras eu quam nibh. Nulla convallis commodo turpis, tempor pulvinar tellus vestibulum eu. Donec vel elit non sapien lacinia ultricies. Fusce sodales quam arcu, condimentum sodales neque posuere et. Cras pulvinar consectetur est. Curabitur aliquam nisl ipsum, vitae bibendum justo facilisis in. Donec sed eleifend turpis, a iaculis arcu. Etiam tincidunt in eros et efficitur. Quisque egestas quis magna nec egestas. Nullam interdum mi sem, accumsan dapibus magna eleifend nec. Nam viverra, lectus in pellentesque tempor, magna purus tempor enim, non facilisis quam nisi at velit. Aenean molestie mauris ac dictum suscipit.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer convallis nibh interdum feugiat imperdiet. Phasellus bibendum tortor sed lectus maximus, in hendrerit mauris volutpat. Cras eu quam nibh. Nulla convallis commodo turpis, tempor pulvinar tellus vestibulum eu. Donec vel elit non sapien lacinia ultricies. Fusce sodales quam arcu, condimentum sodales neque posuere et. Cras pulvinar consectetur est. Curabitur aliquam nisl ipsum, vitae bibendum justo facilisis in. Donec sed eleifend turpis, a iaculis arcu. Etiam tincidunt in eros et efficitur. Quisque egestas quis magna nec egestas. Nullam interdum mi sem, accumsan dapibus magna eleifend nec. Nam viverra, lectus in pellentesque tempor, magna purus tempor enim, non facilisis quam nisi at velit. Aenean molestie mauris ac dictum suscipit.", postTypeExperienceCz, cvutFitLinId,
            //        new PostFileDto[0]), ivanaContext);

            //await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("This was very good course for as an introduction to financial markets. The teachers are really helpful and caring!", postTypeExperienceEn, hseLawFmId, new PostFileDto[0]), ivanaContext);
            //await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("Quick notes from the 3rd class: \n\nDefinition: Financial Market refers to a marketplace, where creation and trading of financial assets, such as shares, debentures, bonds, derivatives, currencies, etc. take place. It plays a crucial role in allocating limited resources, in the country’s economy. It acts as an intermediary between the savers and investors by mobilising funds between them.\r\n\r\nThe financial market provides a platform to the buyers and sellers, to meet, for trading assets at a price determined by the demand and supply forces.\r\n\r\nFunctions of Financial Market\r\nThe functions of the financial market are explained with the help of points below:\r\n\r\nIt facilitates mobilisation of savings and puts it to the most productive uses.\r\nIt helps in determining the price of the securities. The frequent interaction between investors helps in fixing the price of securities, on the basis of their demand and supply in the market.\r\nIt provides liquidity to tradable assets, by facilitating the exchange, as the investors can readily sell their securities and convert assets into cash.\r\nIt saves the time, money and efforts of the parties, as they don’t have to waste resources to find probable buyers or sellers of securities. Further, it reduces cost by providing valuable information, regarding the securities traded in the financial market.\r\nThe financial market may or may not have a physical location, i.e. the exchange of asset between the parties can also take place over the internet or phone also.", postTypeStudyMaterialEn, hseLawFmId, new PostFileDto[0]), petrContext);
            //await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("In the exam there were especially thing from here https://businessjargons.com/financial-market.html#:~:text=Financial%20Market,resources%2C%20in%20the%20country's%20economy. You basically do not need anything else", postTypeStudyMaterialEn, hseLawFmId, new PostFileDto[0]), terezieContext);
            //await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("The course was pretty cool, at least I was quite pleased with that. However, they could work a bit on the organization of it.", postTypeExperienceEn, hseLawFmId, new PostFileDto[0]), terezieContext);
            //await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("Hello, please do not make the same mistake as I did and choose a simple topic for the homework, the teacher will make you thousand times redo it.", postTypeHomeworkEn, hseLawFmId, new PostFileDto[0]), ivanaContext);
            //await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("The wikipedia page https://en.wikipedia.org/wiki/Financial_market is worth checking, even though there might be too much information.", postTypeStudyMaterialEn, hseLawFmId, new PostFileDto[0]), terezieContext);
            //await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("Hi guys,\nThe course is pretty cool. I enjoyed doing the homework, I feel like that could be actually something that will be useful for the future.", postTypeExperienceEn, hseLawFmId, new PostFileDto[0]), barboraContext);

            //await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("This was an internsive course, defenitely think twice before taking it.", postTypeExperienceEn, hseWeFmId, new PostFileDto[0]), barboraContext);
            //await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("Hi, guys for the defense of the homework be fully prepared. She is getting crazy, when you are not sure about something you did in there.", postTypeHomeworkEn, hseWeFmId, new PostFileDto[0]), lucieContext);
            //await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("For studying I recommend this site https://corporatefinanceinstitute.com/resources/knowledge/trading-investing/financial-markets/ It helped me more than the course material, even though some are missing there.", postTypeStudyMaterialEn, hseWeFmId, new PostFileDto[0]), ivanaContext);
            //await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("This is the most important part of the text she gave us to read, you dont really need to read it all:\n\nTypes of Financial Markets\r\nMost people think about the stock market when talking about financial markets. They don't realize there are many kinds that accomplish different goals. Markets exchange a variety of products to help raise liquidity. Each market relies on each other to create confidence in investors. The interconnectedness of these markets means when one suffers, other markets will react accordingly.\r\n\r\nThe Stock Market\r\nThis market is a series of exchanges where successful corporations go to raise large amounts of cash to expand. Stocks are shares of ownership of a public corporation that are sold to investors through broker-dealers. The investors profit when companies increase their earnings. This keeps the U.S. economy growing. It's easy to buy stocks, but it takes a lot of knowledge to buy stocks in the right company.\r\n\r\nTo a lot of people, the Dow is the stock market. The Dow is the nickname for the Dow Jones Industrial Average. The DJIA is just one way of tracking the performance of a group of stocks. There is also the Dow Jones Transportation Average and the Dow Jones Utilities Average. Many investors ignore the Dow and instead focus on the Standard & Poor's 500 index or other indices to track the progress of the stock market. The stocks that make up these averages are traded on the world's stock exchanges, two of which include the New York Stock Exchange (NYSE) and the Nasdaq.", postTypeStudyMaterialEn, hseWeFmId, new PostFileDto[0]), petrContext);

            //await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("The homework is not that hard as it seems just use the materials posted here and you will be more than fine.", postTypeHomeworkEn, hseWeFmId, new PostFileDto[0]), ivanaContext);
            //await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("Dont spend too much time on the homework, focus more on studying for the exam!", postTypeHomeworkEn, hseWeFmId, new PostFileDto[0]), terezieContext);
            //await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("The course itself is nice, but they could improve the way how they actually teach that.", postTypeExperienceEn, hseWeFmId, new PostFileDto[0]), martinContext);


            //// Like posts
            //await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post1.Id), ivanaContext);
            //await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post1.Id), petrContext);
            //await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post1.Id), martinContext);
            //await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post1.Id), terezieContext);

            //await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post2.Id), petrContext);
            //await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post2.Id), lucieContext);
            //await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post2.Id), terezieContext);

            //await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post3.Id), martinContext);
            //await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post3.Id), terezieContext);

            //// Add a few comments
            //var comment1 = (await _addPostCommentServerAction.ExecuteActionAsync(new AddPostCommentRequestDto(post1.Id, "This is absolutely true, more \n\n THAN ANYTHING!!"), ivanaContext)).Post.PostComments.First();
            //var comment2 = (await _addPostCommentServerAction.ExecuteActionAsync(new AddPostCommentRequestDto(post1.Id, "Well hmmm\n mmm \n ok"), ivanaContext)).Post.PostComments.First();
            //var comment3 = (await _addPostCommentServerAction.ExecuteActionAsync(new AddPostCommentRequestDto(post1.Id, "Well hmmm\n this comment is like super big so it will be really hard to put it to exactly one line, so lets see how the user interface will be able to deal with it, but not to make it too long I will just say one simple goodbye man bye have a nice day, ok?"), martinContext)).Post.PostComments.First();

            //// Like comments
            //await _likePostCommentServerAction.ExecuteActionAsync(new LikePostCommentRequestDto(comment1.Id),
            //    ivanaContext);
            //await _likePostCommentServerAction.ExecuteActionAsync(new LikePostCommentRequestDto(comment1.Id),
            //    martinContext);
            //await _likePostCommentServerAction.ExecuteActionAsync(new LikePostCommentRequestDto(comment1.Id),
            //    petrContext);

            //await _likePostCommentServerAction.ExecuteActionAsync(new LikePostCommentRequestDto(comment2.Id),
            //    ivanaContext);
            //await _likePostCommentServerAction.ExecuteActionAsync(new LikePostCommentRequestDto(comment2.Id),
            //    martinContext);

            //await _likePostCommentServerAction.ExecuteActionAsync(new LikePostCommentRequestDto(comment3.Id),
            //    lucieContext);

            // This is here just in case I forget to call it
            _uniwikiContext.SaveChanges();
        }

        public async Task<RequestContext> RegisterUser(string email, string name, string surname, string password, bool isAdmin = false)
        {
            var userId = (await Scoped<RegisterServerAction>().ExecuteActionAsync(
                new RegisterRequestDto(email, name + " " + surname, password, password, true, null, new RecentCourseDto[0]), _anonymousContext)).UserId;

            var user = Scoped<ProfileRepository>().FindById(userId).Single();

            if (isAdmin)
            {
                _profileRepository.SetAsAdmin(user);
            }

            var emailSecret = Scoped<EmailConfirmationSecretRepository>()
                .TryGetValidEmailConfirmationSecret(user.Id);

            await Scoped<ConfirmEmailServerAction>()
                .ExecuteActionAsync(new ConfirmEmailRequestDto(emailSecret!.Secret), _anonymousContext);

            var loginTokenDto = (await Scoped<LoginServerAction>()
                .ExecuteActionAsync(new LoginRequestDto(email, password, new RecentCourseDto[0]), _anonymousContext)).LoginToken;

            var token = Scoped<LoginTokenRepository>().TryFindNonExpiredById(loginTokenDto.PrimaryTokenId, _timeService.Now);

            var context = new RequestContext(token, isAdmin ? AuthenticationLevel.Admin : AuthenticationLevel.PrimaryToken, Guid.NewGuid().ToString(), Language.English, _anonymousContext.IpAddress);

            return context;
        }

        private TDependency Scoped<TDependency>()
        {
            var scope = _serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<TDependency>();
        }

        public Task<AddCourseResponseDto> CreateCourse(string code, string name, Guid studyGroupId, RequestContext requestContext)
        {
            var request = new AddCourseRequestDto(name, code, studyGroupId);
            return Scoped<AddCourseServerAction>().ExecuteActionAsync(request, requestContext);
        }
    }
}
