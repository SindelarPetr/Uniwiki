using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetPostFileRequest : RequestBase<GetPostFileResponse>
    {
        public Guid FileId { get; }
        public string ExpectedName { get; }

        public GetPostFileRequest(Guid fileId, string expectedName)
        {
            FileId = fileId;
            ExpectedName = expectedName;
        }
    }
}