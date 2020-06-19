using System.Linq;
using Shared.Extensions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Authentication;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.RequestResponse.Authentication;
using ProfileModel = Uniwiki.Server.Persistence.Models.ProfileModel;

namespace Uniwiki.Server.Application.Extensions
{
    public static class DomainToDtoExtension
    {
        public static StudyGroupDto ToDto(this StudyGroupModel studyGroup) 
            => new StudyGroupDto(studyGroup.Id, studyGroup.ShortName, studyGroup.LongName, studyGroup.Url, studyGroup.University.ToDto());

        public static CourseDto ToDto(this CourseModel course) 
            => new CourseDto(course.Id, course.FullName, course.Code, course.Url, course.StudyGroup.ToDto(), course.StudyGroup.University.ToDto());

        public static PostFileDto ToDto(this PostFileModel postFile) 
            => new PostFileDto(postFile.Id, postFile.OriginalName, postFile.IsSaved, postFile.Size);

        public static ProfileDto ToDto(this ProfileModel profile) 
            => new ProfileDto(profile.Id, profile.FirstName, profile.FamilyName, profile.FullName, profile.ProfilePictureSrc, profile.CreationDate, profile.Url, profile.Email);

        public static UniversityDto ToDto(this UniversityModel university) 
            => new UniversityDto(university.Id, university.FullName, university.ShortName, university.ShortName.Neutralize());

        public static PostDto ToDto(this PostModel postModel, ProfileModel? clientProfile)
        {
            // If user is not authenticated, then dont include PostFiles
            var postFiles = clientProfile == null
                ? new PostFileDto[0]
                : postModel.Files.Select(f => f.ToDto()).ToArray();

            return new PostDto(postModel.Id, postModel.Author.ToDto(), postModel.Text, postModel.CreationTime, postFiles, postModel.Files.Length, postModel.PostType, postModel.Likes.Count(), postModel.Likes.Any(l => l.Profile == clientProfile), postModel.Comments.Select(m => m.ToDto(clientProfile)).ToArray());
        }

        public static LoginTokenDto ToDto(this LoginTokenModel loginTokenModel) 
            => new LoginTokenDto(loginTokenModel.PrimaryTokenId, loginTokenModel.Expiration, loginTokenModel.SecondaryTokenId);
        
        public static PostCommentDto ToDto(this PostCommentModel commentModel, ProfileModel? clientProfile) 
            => new PostCommentDto(commentModel.Id, commentModel.CreationTime, commentModel.Likes.Count(), commentModel.Profile.ToDto(), commentModel.Text, commentModel.Likes.Any(l => l.Profile == clientProfile));
    }
}
