namespace Shared.RequestResponse
{
    public abstract class RequestBase<TResponse> : RequestBase, IRequest 
        where TResponse : ResponseBase {

    }

    public abstract class RequestBase : IRequest
    {

    }
}