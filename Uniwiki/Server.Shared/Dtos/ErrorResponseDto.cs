namespace Shared.Dtos
{
    public enum ErrorFix { Logout = 1, WrongVersion = 2 }

    public class ErrorResponseDto
    {
        public string Message { get; set; } = null!;

        public ErrorResponseDto(string message)
        {
            Message = message;
        }

        // For serialization
        public ErrorResponseDto()
        {

        }
    }

    public class FixResponseDto
    {
        public string Message { get; set; } = null!;
        public ErrorFix ErrorFix { get; set; }

        public FixResponseDto(string message, ErrorFix errorFix)
        {
            Message = message;
            ErrorFix = errorFix;
        }

        // For serialization
        public FixResponseDto()
        {

        }
    }
}
