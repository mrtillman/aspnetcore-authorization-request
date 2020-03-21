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
  public class GetTokenUseCaseTests
  {
    private GetTokenUseCase getTokenUseCase { get; set; }
    private Mock<ISecureService> mockSecureService { get; set; }
    private Mock<ICacheService> mockCacheService { get; set; }
    
    [TestInitialize]
    public void Initialize(){
      mockSecureService = new Mock<ISecureService>(MockBehavior.Strict);
      mockCacheService = new Mock<ICacheService>(MockBehavior.Strict);
    }

    [TestMethod]
    public async Task Execute_Should_GetAuthorizationResponse() {      
      mockSecureService
          .Setup(service => service.GetToken(It.IsAny<string>(),It.IsAny<string>()))
          .Returns(Task.FromResult(TestDoubles.authResult))
          .Verifiable();
      mockCacheService
          .Setup(service => service.GetValue<AuthorizationResponse>(KEYS.ACCESS_TOKEN))
          .Returns(() => null);
      getTokenUseCase = new GetTokenUseCase(mockSecureService.Object, mockCacheService.Object);

      var result = await getTokenUseCase.Execute();
      
      Assert.IsInstanceOfType(result.Value, typeof(AuthorizationResponse));
      mockSecureService.Verify(service => service.GetToken(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [TestMethod]
    public async Task Execute_Should_GetCachedAuthorizationResponse() {
      mockSecureService
          .Setup(service => service.GetToken(It.IsAny<string>(),It.IsAny<string>()))
          .Verifiable();
      mockCacheService
          .Setup(service => service.GetValue<AuthorizationResponse>(KEYS.ACCESS_TOKEN))
          .Returns(TestDoubles.authResult.Value);
      getTokenUseCase = new GetTokenUseCase(mockSecureService.Object, mockCacheService.Object);

      var result = await getTokenUseCase.Execute();
      
      Assert.IsInstanceOfType(result.Value, typeof(AuthorizationResponse));
      mockSecureService.Verify(service => service.GetToken(It.IsAny<string>(), It.IsAny<string>()), Times.Never);      
    }
  }
}