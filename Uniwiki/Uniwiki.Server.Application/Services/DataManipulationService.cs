using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.ServerActions;
using Uniwiki.Server.Application.ServerActions.Authentication;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.Services
{


    internal class DataManipulationService : IDataManipulationService
    {
        private readonly AddCourseServerAction _addCourseServerAction;
        private readonly RegisterServerAction _registerServerAction;
        private readonly ConfirmEmailServerAction _confirmEmailServerAction;
        private readonly LoginServerAction _loginServerAction;
        private readonly AddCommentServerAction _addCommentServerAction;
        private readonly LikePostCommentServerAction _likePostCommentServerAction;
        private readonly LikePostServerAction _likePostServerAction;
        private readonly IUniversityRepository _universityRepository;
        private readonly AddStudyGroupServerAction _addStudyGroupServerAction;
        private readonly AddPostServerAction _addPostServerAction;
        private readonly ILoginTokenRepository _loginTokenRepository;
        private readonly IEmailConfirmationSecretRepository _emailConfirmationSecretRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly ITimeService _timeService;
        private readonly IEmailService _emailService;
        private readonly RequestContext _anonymousContext = new RequestContext(null, AuthenticationLevel.None, Guid.NewGuid().ToString(), Language.English, new System.Net.IPAddress(0x2414188));

        public DataManipulationService(AddCourseServerAction addCourseServerAction, RegisterServerAction registerServerAction, ConfirmEmailServerAction confirmEmailServerAction, LoginServerAction loginServerAction, AddCommentServerAction addCommentServerAction, LikePostCommentServerAction likePostCommentServerAction, LikePostServerAction likePostServerAction, IUniversityRepository universityRepository, AddStudyGroupServerAction addStudyGroupServerAction, AddPostServerAction addPostServerAction, ILoginTokenRepository loginTokenRepository, IEmailConfirmationSecretRepository emailConfirmationSecretRepository, IProfileRepository profileRepository, ITimeService timeService, IEmailService emailService)
        {
            _addCourseServerAction = addCourseServerAction;
            _registerServerAction = registerServerAction;
            _confirmEmailServerAction = confirmEmailServerAction;
            _loginServerAction = loginServerAction;
            _addCommentServerAction = addCommentServerAction;
            _likePostCommentServerAction = likePostCommentServerAction;
            _likePostServerAction = likePostServerAction;
            _universityRepository = universityRepository;
            _addStudyGroupServerAction = addStudyGroupServerAction;
            _addPostServerAction = addPostServerAction;
            _loginTokenRepository = loginTokenRepository;
            _emailConfirmationSecretRepository = emailConfirmationSecretRepository;
            _profileRepository = profileRepository;
            _timeService = timeService;
            _emailService = emailService;
        }

        public async Task InitializeFakeData()
        {
            // Make sure no emails will be sent
            _emailService.DisableSendingEmails();

            // Register users
            var lucieContext = await RegisterUser("a@a.cz", "Lucie", "Veselá", "aaaaaa", true);
            var ivanaContext = await RegisterUser("b@b.cz", "Ivana", "Nováková", "aaaaaa");
            var terezieContext = await RegisterUser("c@c.cz", "Marie", "Terezie", "aaaaaa");
            var petrContext = await RegisterUser("d@d.cz", "Petr", "Šindelář", "aaaaaa");
            var barboraContext = await RegisterUser("e@e.cz", "Barbora", "Zelená", "aaaaaa");
            var martinContext = await RegisterUser("f@f.cz", "Martin", "Novák", "aaaaaa");

            // Universities
            var uniCvut = _universityRepository.CreateUniversity("České vysoké učení technické v Praze", "ČVUT", "cvut");
            var uniVse = _universityRepository.CreateUniversity("Vysoká škola ekonomická", "VŠE", "vse");
            var uniCzu = _universityRepository.CreateUniversity("Česká zemědělská universita", "ČZU", "czu");
            var uniHse = _universityRepository.CreateUniversity("Higher school of economics", "HSE", "hse");
            var uniUof = _universityRepository.CreateUniversity("University of Oxford", "UOF", "uof");

            // Study groups (faculties)
            var fitCvut = (await _addStudyGroupServerAction.ExecuteActionAsync(
                new AddStudyGroupRequestDto("Fakulta informačních technologií", "FIT", uniCvut.Id, Language.Czech), lucieContext)).StudyGroupDto;
            var ecoHse = (await _addStudyGroupServerAction.ExecuteActionAsync(
                new AddStudyGroupRequestDto("Faculty of Economic Sciences", "FES", uniHse.Id, Language.English), lucieContext)).StudyGroupDto;
            var lawHse = (await _addStudyGroupServerAction.ExecuteActionAsync(
                new AddStudyGroupRequestDto("Faculty of Law", "FL", uniHse.Id, Language.English), lucieContext)).StudyGroupDto;
            var weHse = (await _addStudyGroupServerAction.ExecuteActionAsync(
                new AddStudyGroupRequestDto("Faculty of World Economy and International Affairs", "WE", uniHse.Id, Language.English), lucieContext)).StudyGroupDto;
            var fcsHse = (await _addStudyGroupServerAction.ExecuteActionAsync(
                new AddStudyGroupRequestDto("Faculty of Computer Science", "FCS", uniHse.Id, Language.English), lucieContext)).StudyGroupDto;
            var fmHse = (await _addStudyGroupServerAction.ExecuteActionAsync(
                new AddStudyGroupRequestDto("Faculty of Mathematics", "FM", uniHse.Id, Language.English), lucieContext)).StudyGroupDto;
            var fssHse = (await _addStudyGroupServerAction.ExecuteActionAsync(
                new AddStudyGroupRequestDto("Faculty of Social Sciences", "FSS", uniHse.Id, Language.English), lucieContext)).StudyGroupDto;
            var fhHse = (await _addStudyGroupServerAction.ExecuteActionAsync(
                new AddStudyGroupRequestDto("Faculty of Humanities", "FH", uniHse.Id, Language.English), lucieContext)).StudyGroupDto;
            var fbmHse = (await _addStudyGroupServerAction.ExecuteActionAsync(
                new AddStudyGroupRequestDto("Faculty of Business and Management", "FBM", uniHse.Id, Language.English), lucieContext)).StudyGroupDto;

            // Courses

            // CVUT FIT
            await CreateCourse("BI-3D", "3D Tisk", fitCvut, lucieContext);
            await CreateCourse("BI-EMP", "Ekonomické principy a management", fitCvut, lucieContext);
            await CreateCourse("BI-CS1", "Programování v C# 1", fitCvut, lucieContext);
            await CreateCourse("BI-CS2", "Programování v C# 2", fitCvut, lucieContext);
            await CreateCourse("BI-CS3", "Programování v C# 3", fitCvut, lucieContext);
            await CreateCourse("BI-AAG", "Automaty a gramatiky", fitCvut, lucieContext);
            await CreateCourse("BI-AG1", "Algoritmy a grafy 1", fitCvut, lucieContext);
            await CreateCourse("BI-BEZ", "Bezpečnost", fitCvut, lucieContext);
            await CreateCourse("BI-CAO", "Číslicové a analogové obvody", fitCvut, lucieContext);
            await CreateCourse("BI-DBS", "Databázové systémy", fitCvut, lucieContext);
            await CreateCourse("BI-MLO", "Matematická logika", fitCvut, lucieContext);
            await CreateCourse("BI-PA1", "Programování a algoritmizace 1", fitCvut, lucieContext);
            await CreateCourse("BI-PA2", "Programování a algoritmizace 2", fitCvut, lucieContext);
            await CreateCourse("BI-OSY", "Operační systémy", fitCvut, ivanaContext);
            var cvutFitLin = (await CreateCourse("BI-LIN", "Lineární algebra", fitCvut, lucieContext)).CourseDto;

            // HSE FE courses
            await CreateCourse("", "Economics of Natural Resources", ecoHse, lucieContext);
            await CreateCourse("", "Game Theory", ecoHse, lucieContext);
            await CreateCourse("", "Digital Transformation of the World Economy", ecoHse, ivanaContext);
            await CreateCourse("", "Mergers, Acquisitions and Restructuring of a Firm", ecoHse, lucieContext);
            await CreateCourse("", "Mergers and Acquisitions in Financial Markets", ecoHse, lucieContext);
            await CreateCourse("", "Microeconomic Methods of Economic Policy Analysis", ecoHse, lucieContext);
            await CreateCourse("", "Models with Qualitative Dependent Variables", ecoHse, lucieContext);
            await CreateCourse("", "Microeconomics: applications", ecoHse, ivanaContext);
            await CreateCourse("", "Microeconomics", ecoHse, lucieContext);
            await CreateCourse("", "Personnel Economics", ecoHse, lucieContext);
            await CreateCourse("", "Personal Money Management", ecoHse, lucieContext);
            await CreateCourse("", "Portfolio Management", ecoHse, ivanaContext);
            await CreateCourse("", "Principles of Corporate Finance", ecoHse, ivanaContext);
            await CreateCourse("", "Econometrics of Program Evaluation", ecoHse, ivanaContext);
            await CreateCourse("", "Empirical Industrial Organisations", ecoHse, lucieContext);
            await CreateCourse("", "English for Financiers (Advanced Level)", ecoHse, lucieContext);
            await CreateCourse("", "Financial Innovation", ecoHse, lucieContext);
            await CreateCourse("", "Financial Markets:Problems and Decisions", ecoHse, lucieContext);
            await CreateCourse("", "Fundamental and Technical Analysis", ecoHse, lucieContext);
            await CreateCourse("", "Advanced Microeconomics", ecoHse, ivanaContext);
            await CreateCourse("", "Behavioral Finance", ecoHse, lucieContext);
            await CreateCourse("", "Effective economics", ecoHse, lucieContext);

            // HSE ECO
            var egFeHse = (await CreateCourse("", "Economic geography", ecoHse, lucieContext)).CourseDto;

            // HSE FCS
            await CreateCourse("", "Effective economics", fcsHse, lucieContext);

            // HSE FM
            await CreateCourse("", "Effective economics", fmHse, lucieContext);

            // HSE FSS
            await CreateCourse("", "Effective economics", fssHse, lucieContext);

            // HSE FH
            await CreateCourse("", "Effective economics", fhHse, lucieContext);

            // HSE FBM
            await CreateCourse("", "Effective economics", fbmHse, lucieContext);

            // HSE Law
            await CreateCourse("", "Administrative Law", lawHse, lucieContext);
            await CreateCourse("", "Academic English Writing", lawHse, lucieContext);
            await CreateCourse("", "English Contract Law", lawHse, lucieContext);
            await CreateCourse("", "Commercial Procedure", lawHse, lucieContext);
            await CreateCourse("", "Bankruptcy Businesses", lawHse, lucieContext);
            await CreateCourse("", "Safe Living Basics", lawHse, lucieContext);
            await CreateCourse("", "An Introduction to American Law", lawHse, lucieContext);
            await CreateCourse("", "Civil Law", lawHse, lucieContext);
            await CreateCourse("", "Land Law", lawHse, lucieContext);
            await CreateCourse("", "Internet Giants: The Law and Economics of Media Platforms", lawHse, lucieContext);
            await CreateCourse("", "Information Law", lawHse, lucieContext);
            await CreateCourse("", "Information Technologies in the Activity of Lawyer", lawHse, lucieContext);
            await CreateCourse("", "History of Political and Legal Doctrines", lawHse, lucieContext);
            await CreateCourse("", "Competition Law", lawHse, lucieContext);
            await CreateCourse("", "Constitutional Law of Foreign Countries", lawHse, lucieContext);
            await CreateCourse("", "Constitutional Challenges in the Islamic World", lawHse, lucieContext);
            await CreateCourse("", "Forensic Science", lawHse, lucieContext);
            await CreateCourse("", "Criminology", lawHse, lucieContext);
            var hseLawFm = (await CreateCourse("", "Financial Markets", lawHse, lucieContext)).CourseDto;
            var hseLawM2 = (await CreateCourse("", "Advanced Management", lawHse, lucieContext)).CourseDto;
            await CreateCourse("", "International Law in Action: the Arbitration of International Disputes", lawHse, lucieContext);
            await CreateCourse("", "International Law in Action: A Guide to the International Courts and Tribunals in The Hague", lawHse, lucieContext);


            // HSE WE
            await CreateCourse("", "Academic English Writing", weHse, lucieContext);
            await CreateCourse("", "Introduction to International Affairs and World Economy", weHse, lucieContext);
            await CreateCourse("", "Global Challenges and Issues", weHse, lucieContext);
            await CreateCourse("", "Foreign Language (English for Professional Purposes)", weHse, lucieContext);
            await CreateCourse("", "Foreign Language (Arabic)", weHse, lucieContext);
            await CreateCourse("", "International Culture", weHse, lucieContext);
            await CreateCourse("", "Research Seminar \"Key Tendencies of the Modern Middle East and Northern Africa Development\"", weHse, lucieContext);
            await CreateCourse("", "Research Seminar \"Methods in International Studies\"", weHse, lucieContext);
            await CreateCourse("", "Scientific Seminar The Euro Atlantic Region: Economic and Political Problems", weHse, lucieContext);
            await CreateCourse("", "National and International Security", weHse, lucieContext);
            await CreateCourse("", "National and Multilateral Governance", weHse, lucieContext);
            await CreateCourse("", "Key development tendencies of Eurasia and Trans-Pacific Region", weHse, lucieContext);
            await CreateCourse("", "Introduction to International Business", weHse, lucieContext);
            await CreateCourse("", "Political history of the Middle East", weHse, lucieContext);
            await CreateCourse("", "Political History of China", weHse, lucieContext);
            await CreateCourse("", "Political History of USA", weHse, lucieContext);
            await CreateCourse("", "Law", weHse, lucieContext);
            await CreateCourse("", "Economy and Politics of Arab Countries", weHse, lucieContext);
            await CreateCourse("", "Energy policy and Diplomacy", weHse, lucieContext);
            var hseWeFm = (await CreateCourse("", "Financial Markets", weHse, lucieContext)).CourseDto;
            var hseWeM2 = (await CreateCourse("", "Advanced Management", weHse, lucieContext)).CourseDto;


            var postTypeExperienceEn = "Experience";
            var postTypeExperienceCz = "Zkušenost";
            var postTypeStudyMaterialCz = "Studijní materiál";
            var postTypeStudyMaterialEn = "Study material";
            var postTypeHomeworkCz = "Domácí úkol";
            var postTypeHomeworkEn = "Homework";

            var post1 = (await _addPostServerAction.ExecuteActionAsync(
                new AddPostRequestDto("This was the best course so far", postTypeExperienceCz, cvutFitLin.Id,
                    new PostFileDto[0]), lucieContext)).NewPost;
            var post2 = (await _addPostServerAction.ExecuteActionAsync(
                new AddPostRequestDto("The course is quite cool", postTypeStudyMaterialCz, cvutFitLin.Id,
                    new PostFileDto[0]), petrContext)).NewPost;
            var post3 = (await _addPostServerAction.ExecuteActionAsync(
                new AddPostRequestDto("Teachers are really nice. But the syllabus (or whatever the spelling is - I dont care) quite sucks. I think you guys should work on it more. For a person who have never studied mathematics this was wayyy to hard.", postTypeExperienceCz, cvutFitLin.Id,
                    new PostFileDto[0]), ivanaContext)).NewPost;
            await _addPostServerAction.ExecuteActionAsync(
                new AddPostRequestDto("Teachers are really nice. But the syllabus (or whatever the spelling is - I dont care) quite sucks. I think you guys should work on it more. For a person who have never studied mathematics this was wayyy to hard.", postTypeExperienceCz, cvutFitLin.Id,
                    new PostFileDto[0]), terezieContext);
            await _addPostServerAction.ExecuteActionAsync(
                new AddPostRequestDto("The classes are super cool, I enjoyed every minute.", postTypeStudyMaterialCz, cvutFitLin.Id,
                    new PostFileDto[0]), barboraContext);
            await _addPostServerAction.ExecuteActionAsync(
                new AddPostRequestDto("This is the homework for the 3rd class, really good experience btw. I enjoyed it so much.", postTypeExperienceCz, cvutFitLin.Id,
                    new PostFileDto[0]), martinContext);
            await _addPostServerAction.ExecuteActionAsync(
                new AddPostRequestDto("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer convallis nibh interdum feugiat imperdiet. Phasellus bibendum tortor sed lectus maximus, in hendrerit mauris volutpat. Cras eu quam nibh. Nulla convallis commodo turpis, tempor pulvinar tellus vestibulum eu. Donec vel elit non sapien lacinia ultricies. Fusce sodales quam arcu, condimentum sodales neque posuere et. Cras pulvinar consectetur est. Curabitur aliquam nisl ipsum, vitae bibendum justo facilisis in. Donec sed eleifend turpis, a iaculis arcu. Etiam tincidunt in eros et efficitur. Quisque egestas quis magna nec egestas. Nullam interdum mi sem, accumsan dapibus magna eleifend nec. Nam viverra, lectus in pellentesque tempor, magna purus tempor enim, non facilisis quam nisi at velit. Aenean molestie mauris ac dictum suscipit.", postTypeExperienceCz, cvutFitLin.Id,
                    new PostFileDto[0]), martinContext);
            await _addPostServerAction.ExecuteActionAsync(
                new AddPostRequestDto("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer convallis nibh interdum feugiat imperdiet. Phasellus bibendum tortor sed lectus maximus, in hendrerit mauris volutpat. Cras eu quam nibh. Nulla convallis commodo turpis, tempor pulvinar tellus vestibulum eu. Donec vel elit non sapien lacinia ultricies. Fusce sodales quam arcu, condimentum sodales neque posuere et. Cras pulvinar consectetur est. Curabitur aliquam nisl ipsum, vitae bibendum justo facilisis in. Donec sed eleifend turpis, a iaculis arcu. Etiam tincidunt in eros et efficitur. Quisque egestas quis magna nec egestas. Nullam interdum mi sem, accumsan dapibus magna eleifend nec. Nam viverra, lectus in pellentesque tempor, magna purus tempor enim, non facilisis quam nisi at velit. Aenean molestie mauris ac dictum suscipit.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer convallis nibh interdum feugiat imperdiet. Phasellus bibendum tortor sed lectus maximus, in hendrerit mauris volutpat. Cras eu quam nibh. Nulla convallis commodo turpis, tempor pulvinar tellus vestibulum eu. Donec vel elit non sapien lacinia ultricies. Fusce sodales quam arcu, condimentum sodales neque posuere et. Cras pulvinar consectetur est. Curabitur aliquam nisl ipsum, vitae bibendum justo facilisis in. Donec sed eleifend turpis, a iaculis arcu. Etiam tincidunt in eros et efficitur. Quisque egestas quis magna nec egestas. Nullam interdum mi sem, accumsan dapibus magna eleifend nec. Nam viverra, lectus in pellentesque tempor, magna purus tempor enim, non facilisis quam nisi at velit. Aenean molestie mauris ac dictum suscipit.", postTypeExperienceCz, cvutFitLin.Id,
                    new PostFileDto[0]), ivanaContext);

            await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("This was very good course for as an introduction to financial markets. The teachers are really helpful and caring!", postTypeExperienceEn, hseLawFm.Id, new PostFileDto[0]), ivanaContext);
            await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("Quick notes from the 3rd class: \n\nDefinition: Financial Market refers to a marketplace, where creation and trading of financial assets, such as shares, debentures, bonds, derivatives, currencies, etc. take place. It plays a crucial role in allocating limited resources, in the country’s economy. It acts as an intermediary between the savers and investors by mobilising funds between them.\r\n\r\nThe financial market provides a platform to the buyers and sellers, to meet, for trading assets at a price determined by the demand and supply forces.\r\n\r\nFunctions of Financial Market\r\nThe functions of the financial market are explained with the help of points below:\r\n\r\nIt facilitates mobilisation of savings and puts it to the most productive uses.\r\nIt helps in determining the price of the securities. The frequent interaction between investors helps in fixing the price of securities, on the basis of their demand and supply in the market.\r\nIt provides liquidity to tradable assets, by facilitating the exchange, as the investors can readily sell their securities and convert assets into cash.\r\nIt saves the time, money and efforts of the parties, as they don’t have to waste resources to find probable buyers or sellers of securities. Further, it reduces cost by providing valuable information, regarding the securities traded in the financial market.\r\nThe financial market may or may not have a physical location, i.e. the exchange of asset between the parties can also take place over the internet or phone also.", postTypeStudyMaterialEn, hseLawFm.Id, new PostFileDto[0]), petrContext);
            await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("In the exam there were especially thing from here https://businessjargons.com/financial-market.html#:~:text=Financial%20Market,resources%2C%20in%20the%20country's%20economy. You basically do not need anything else", postTypeStudyMaterialEn, hseLawFm.Id, new PostFileDto[0]), terezieContext);
            await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("The course was pretty cool, at least I was quite pleased with that. However, they could work a bit on the organization of it.", postTypeExperienceEn, hseLawFm.Id, new PostFileDto[0]), terezieContext);
            await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("Hello, please do not make the same mistake as I did and choose a simple topic for the homework, the teacher will make you thousand times redo it.", postTypeHomeworkEn, hseLawFm.Id, new PostFileDto[0]), ivanaContext);
            await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("The wikipedia page https://en.wikipedia.org/wiki/Financial_market is worth checking, even though there might be too much information.", postTypeStudyMaterialEn, hseLawFm.Id, new PostFileDto[0]), terezieContext);
            await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("Hi guys,\nThe course is pretty cool. I enjoyed doing the homework, I feel like that could be actually something that will be useful for the future.", postTypeExperienceEn, hseLawFm.Id, new PostFileDto[0]), barboraContext);

            await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("This was an internsive course, defenitely think twice before taking it.", postTypeExperienceEn, hseWeFm.Id, new PostFileDto[0]), barboraContext);
            await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("Hi, guys for the defense of the homework be fully prepared. She is getting crazy, when you are not sure about something you did in there.", postTypeHomeworkEn, hseWeFm.Id, new PostFileDto[0]), lucieContext);
            await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("For studying I recommend this site https://corporatefinanceinstitute.com/resources/knowledge/trading-investing/financial-markets/ It helped me more than the course material, even though some are missing there.", postTypeStudyMaterialEn, hseWeFm.Id, new PostFileDto[0]), ivanaContext);
            await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("This is the most important part of the text she gave us to read, you dont really need to read it all:\n\nTypes of Financial Markets\r\nMost people think about the stock market when talking about financial markets. They don't realize there are many kinds that accomplish different goals. Markets exchange a variety of products to help raise liquidity. Each market relies on each other to create confidence in investors. The interconnectedness of these markets means when one suffers, other markets will react accordingly.\r\n\r\nThe Stock Market\r\nThis market is a series of exchanges where successful corporations go to raise large amounts of cash to expand. Stocks are shares of ownership of a public corporation that are sold to investors through broker-dealers. The investors profit when companies increase their earnings. This keeps the U.S. economy growing. It's easy to buy stocks, but it takes a lot of knowledge to buy stocks in the right company.\r\n\r\nTo a lot of people, the Dow is the stock market. The Dow is the nickname for the Dow Jones Industrial Average. The DJIA is just one way of tracking the performance of a group of stocks. There is also the Dow Jones Transportation Average and the Dow Jones Utilities Average. Many investors ignore the Dow and instead focus on the Standard & Poor's 500 index or other indices to track the progress of the stock market. The stocks that make up these averages are traded on the world's stock exchanges, two of which include the New York Stock Exchange (NYSE) and the Nasdaq.", postTypeStudyMaterialEn, hseWeFm.Id, new PostFileDto[0]), petrContext);

          await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("The homework is not that hard as it seems just use the materials posted here and you will be more than fine.", postTypeHomeworkEn, hseWeFm.Id, new PostFileDto[0]), ivanaContext);
            await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("Dont spend too much time on the homework, focus more on studying for the exam!", postTypeHomeworkEn, hseWeFm.Id, new PostFileDto[0]), terezieContext);
            await _addPostServerAction.ExecuteActionAsync(new AddPostRequestDto("The course itself is nice, but they could improve the way how they actually teach that.", postTypeExperienceEn, hseWeFm.Id, new PostFileDto[0]), martinContext);


            // Like posts
            await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post1.Id), ivanaContext);
            await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post1.Id), petrContext);
            await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post1.Id), martinContext);
            await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post1.Id), terezieContext);

            await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post2.Id), petrContext);
            await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post2.Id), lucieContext);
            await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post2.Id), terezieContext);

            await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post3.Id), martinContext);
            await _likePostServerAction.ExecuteActionAsync(new LikePostRequestDto(post3.Id), terezieContext);

            // Add a few comments
            var comment1 = (await _addCommentServerAction.ExecuteActionAsync(new AddCommentRequestDto(post1.Id, "This is absolutely true, more \n\n THAN ANYTHING!!"), ivanaContext)).Post.PostComments.First();
            var comment2 = (await _addCommentServerAction.ExecuteActionAsync(new AddCommentRequestDto(post1.Id, "Well hmmm\n mmm \n ok"), ivanaContext)).Post.PostComments.First();
            var comment3 = (await _addCommentServerAction.ExecuteActionAsync(new AddCommentRequestDto(post1.Id, "Well hmmm\n this comment is like super big so it will be really hard to put it to exactly one line, so lets see how the user interface will be able to deal with it, but not to make it too long I will just say one simple goodbye man bye have a nice day, ok?"), martinContext)).Post.PostComments.First();

            // Like comments
            await _likePostCommentServerAction.ExecuteActionAsync(new LikePostCommentRequestDto(comment1.Id),
                ivanaContext);
            await _likePostCommentServerAction.ExecuteActionAsync(new LikePostCommentRequestDto(comment1.Id),
                martinContext);
            await _likePostCommentServerAction.ExecuteActionAsync(new LikePostCommentRequestDto(comment1.Id),
                petrContext);

            await _likePostCommentServerAction.ExecuteActionAsync(new LikePostCommentRequestDto(comment2.Id),
                ivanaContext);
            await _likePostCommentServerAction.ExecuteActionAsync(new LikePostCommentRequestDto(comment2.Id),
                martinContext);

            await _likePostCommentServerAction.ExecuteActionAsync(new LikePostCommentRequestDto(comment3.Id),
                lucieContext);
        }

        public async Task<RequestContext> RegisterUser(string email, string name, string surname, string password, bool isAdmin = false)
        {
            var userDto = (await _registerServerAction.ExecuteActionAsync(new RegisterRequestDto(email, name + " " + surname, password, password, true, null, new CourseDto[0]), _anonymousContext)).UserProfile;
            var user = _profileRepository.FindById(userDto.Id);

            if (isAdmin)
                _profileRepository.SetAdmin(user);

            var emailSecret = _emailConfirmationSecretRepository.TryGetValidEmailConfirmationSecret(user);
            await _confirmEmailServerAction.ExecuteActionAsync(new ConfirmEmailRequestDto(emailSecret.Id),
                _anonymousContext);
            var loginTokenDto = (await _loginServerAction.ExecuteActionAsync(new LoginRequestDto(email, password, new CourseDto[0]), _anonymousContext)).LoginToken;
            var token = _loginTokenRepository.TryFindById(loginTokenDto.PrimaryTokenId, _timeService.Now);
            
            return new RequestContext(token, isAdmin ? AuthenticationLevel.Admin : AuthenticationLevel.PrimaryToken, Guid.NewGuid().ToString(), Language.English, _anonymousContext.IpAddress);
        }

        public Task<AddCourseResponseDto> CreateCourse(string code, string name, StudyGroupDto studyGroup, RequestContext requestContext)
        {
            var request = new AddCourseRequestDto(name, code, studyGroup.Id);
            return _addCourseServerAction.ExecuteActionAsync(request, requestContext);
        }
    }
}
