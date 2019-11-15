using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Application;
using Common;
using Domain;
using Services;

namespace Tests.Application
{
  [TestClass]
  public class GetTokenTests
  {
    public GetTokenUseCase getTokenUseCase { get; set; }

    [TestMethod]
    public async Task Should_Get_Token() {
      var mockSecureService = Mock.Of<ISecureService>(Moq.MockBehavior.Strict);
      Result<AuthorizationResponse> mockResult = Result<AuthorizationResponse>.Ok(new AuthorizationResponse());
      Mock.Get(mockSecureService)
          .Setup(api => api.GetToken(It.IsAny<string>(),It.IsAny<string>()))
          .Returns(Task.FromResult(mockResult));
      getTokenUseCase = new GetTokenUseCase(mockSecureService);
      var result = await getTokenUseCase.Execute();
      Assert.IsTrue(result.DidSucceed);
    }
  }
}