using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Server.Application.Tests
{
    [TestClass]
    public class PipelineModuleTests
    {
        //[TestMethod]
        //public void ServerActionProviderCanFindServerActions()
        //{
        //    // Arrange
        //    var provider = new ServerActionProvider();

        //    // Act
        //    var serverActions = provider.ProvideRequestsAndServerActions();

        //    // Assert
        //    var serverActionType = serverActions[typeof(FakeRequest)];
        //    Assert.IsNotNull(serverActionType);
        //    Assert.AreEqual(serverActionType, typeof(FakeServerAction));
        //}

        //[TestMethod]
        //public async Task PipelineCanInvokeAServerAction()
        //{
        //    // Arrange
        //    // var provider = new ServerActionProvider();
        //    var resolver = // TODO: Import the fake resolver of the server actions // new ServerActionResolver(provider);
        //    var pipeline = new PipelineModule(resolver);
        //    var numberA = 3;
        //    var numberB = 5;
        //    var serverRequest = new ServerRequest<IRequest>(Guid.Empty, new FakeRequest(numberA, numberB), null);

        //    // Act
        //    await pipeline.Start();
        //    var response = await pipeline.ProcessRequestAsync(serverRequest);
        //    await pipeline.End();

        //    // Assert
        //    Assert.IsTrue(response is FakeResponse);
        //    Assert.AreEqual(numberA + numberB, ((FakeResponse)response).NumberAB);
        //}

    }

    //public class FakeRequest : RequestBase<FakeResponse>
    //{
    //    public int NumberA { get; }
    //    public int NumberB { get; }

    //    public FakeRequest(int numberA, int numberB)
    //    {
    //        NumberA = numberA;
    //        NumberB = numberB;
    //    }
    //}

    //public class FakeResponse : ResponseBase
    //{
    //    public int NumberAB { get; }

    //    public FakeResponse(int numberAb)
    //    {
    //        NumberAB = numberAb;
    //    }
    //}

    //public class FakeServerAction : ServerActionBase<FakeRequest,FakeResponse>
    //{
    //    protected override Task<FakeResponse> ExecuteAsync(ServerRequest<FakeRequest> serverRequest)
    //    {
    //        var resultAB = serverRequest.Request.NumberA + serverRequest.Request.NumberB;
    //        return Task.FromResult(new FakeResponse(resultAB));
    //    }

    //    protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.PrimaryToken;
    //    protected override Task<FakeResponse> ExecuteAsync(FakeRequest request, RequestContext context)
    //    {
            
    //    }

    //    public FakeServerAction(IServiceProvider serviceProvider) : base(serviceProvider)
    //    {
    //    }
    //}
}
