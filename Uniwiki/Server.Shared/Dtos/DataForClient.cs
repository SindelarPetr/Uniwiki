using Shared.RequestResponse;

namespace Shared.Dtos
{
    public class DataForClient<T> where T : class, IResponse
    {
        public FixResponseDto[] Fixes { get; set; }
        public ErrorResponseDto? Error { get; set; }
        public T? Response { get; set; }

        // For Serialization
        public DataForClient()
        {

        }

        public DataForClient(T response, FixResponseDto[]? fixes = null)
        {
            Response = response;
            Fixes = fixes ?? new FixResponseDto[0];
        }

        public DataForClient(ErrorResponseDto error, FixResponseDto[]? fixes = null)
        {
            Error = error;
            Fixes = fixes ?? new FixResponseDto[0];
        }
    }
}
