using System.Collections.Generic;
using System.Linq;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Authentication;
using ProfileModel = Uniwiki.Server.Persistence.Models.ProfileModel;

namespace Uniwiki.Server.Persistence.InMemory.Services
{
    // We dont want to create an interface for this, because we dont want to abstract it in any way
    internal class DataService
    {
        public List<LoginTokenModel> LoginTokens { get; set; } = new List<LoginTokenModel>();
        public List<NewPasswordSecretModel> NewPasswordSecrets { get; set; } = new List<NewPasswordSecretModel>();
        public List<EmailConfirmationSecretModel> EmailConfirmationSecrets { get; set; } = new List<EmailConfirmationSecretModel>();
        public List<ProfileModel> Profiles { get; set; } = new List<ProfileModel>();
        public List<UniversityModel> Universities { get; set; } = new List<UniversityModel>();
        public List<StudyGroupModel> StudyGroups { get; set; } = new List<StudyGroupModel>();
        public List<CourseVisitModel> CourseVisits { get; set; } = new List<CourseVisitModel>();
        public IEnumerable<CourseModel> Courses => _courses.OrderBy(c => c.FullName);
        public List<CourseModel> _courses = new List<CourseModel>();
        public IEnumerable<PostModel> Posts => _posts.Where(p => !p.IsRemoved);
        public List<PostModel> _posts = new List<PostModel>();
        public List<PostFileModel> PostFiles { get; set; } = new List<PostFileModel>();
        public IEnumerable<PostCommentModel> PostComments => _postComments.Where(c => !c.IsRemoved);
        public List<PostCommentModel> _postComments = new List<PostCommentModel>();
        public IEnumerable<PostLikeModel> PostLikes => _postLikes.Where(l => !l.IsRemoved);
        public List<PostLikeModel> _postLikes = new List<PostLikeModel>();
        public IEnumerable<PostCommentLikeModel> PostCommentLikes => _postCommentLikes.Where(c => !c.IsRemoved);
        public List<PostCommentLikeModel> _postCommentLikes = new List<PostCommentLikeModel>();

        public IEnumerable<PostFileDownloadModel> PostFileDownloads => _postFileDownloads.OrderBy(d => d.DownloadTime);
        public List<PostFileDownloadModel> _postFileDownloads = new List<PostFileDownloadModel>();

        public List<FeedbackModel> Feedbacks = new List<FeedbackModel>();

        public IEnumerable<string> _defaultPostTypesCz => new[]
        {
            "Domácí úkol",
            "Test v semesteru",
            "Zkouška",
            "Studijní materiál"
        };

        public IEnumerable<string> _defaultPostTypesEn => new[]
        {
            "Homework",
                "Test in semester",
                "Final exam",
                "Study material"
        };
    }
}
