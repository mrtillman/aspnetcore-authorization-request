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
      var mockSecureApi = Mock.Of<ISecureApi>(Moq.MockBehavior.Strict);
      Result<AuthorizationResponse> mockResult = Result<AuthorizationResponse>.Ok(new AuthorizationResponse());
      Mock.Get(mockSecureApi)
          .Setup(api => api.GetToken(It.IsAny<string>(),It.IsAny<string>()))
          .Returns(Task.FromResult(mockResult));
      getTokenUseCase = new GetTokenUseCase(mockSecureApi);
      var result = await getTokenUseCase.Execute();
      Assert.IsTrue(result.DidSucceed);
    }
  }
}