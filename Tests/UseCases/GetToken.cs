using AuthDemo.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuthDemo.Interfaces;
using AuthDemo.Models;
using Moq;
using System.Threading.Tasks;

namespace AuthDemo.Tests
{
  [TestClass]
  public class GetTokenTests
  {
    public GetTokenUseCase getTokenUseCase { get; set; }

    [TestMethod]
    public async Task Should_Get_Token() {
      var mockSecureApi = Mock.Of<ISecureApi>();
      Result<AuthResponse> mockResult = Result<AuthResponse>.Ok(new AuthResponse());
      Mock.Get(mockSecureApi)
          .Setup(api => api.GetToken(It.IsAny<string>(),It.IsAny<string>()))
          .Returns(Task.FromResult(mockResult));
      getTokenUseCase = new GetTokenUseCase(mockSecureApi);
      var result = await getTokenUseCase.Execute();
      Assert.IsTrue(result.DidSucceed);
    }
  }
}