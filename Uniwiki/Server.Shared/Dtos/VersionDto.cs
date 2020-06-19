namespace Shared.Dtos
{
    public class VersionDto
    {
        public string VersionString { get; set; }

        public VersionDto(string versionString)
        {
            VersionString = versionString;
        }

        // For serialization
        public VersionDto()
        {
            
        }
    }
}