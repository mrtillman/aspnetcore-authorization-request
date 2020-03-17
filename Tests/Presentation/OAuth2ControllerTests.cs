using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation;
using Application;
using Moq;
using Domain;
using Common;
using System.Threading.Tasks;
using Services;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Presentation {
  
  [TestClass]
  public class OAuth2ControllerTests
  {
    
    public OAuth2Controller controller { get; set; }
    public RenewTokenUseCase renewTokenUseCase { get; set; }
    private Mock<ICacheService> cacheServiceMock { get; set; }
    private Mock<ISecureService> secureServiceMock { get; set; }

    [TestMethod]
    public async Task RenewTokenShould_RedirectHome_WhenRefreshTokenIsNull(){
      cacheServiceMock = new Mock<ICacheService>();
      cacheServiceMock.Setup(cache => cache.GetValue<string>(KEYS.REFRESH_TOKEN))
                      .Returns(() => null);
      renewTokenUseCase = new RenewTokenUseCase(null, cacheServiceMock.Object);
      renewTokenUseCase.RefreshToken = null;
      controller = new OAuth2Controller(
        null, null, renewTokenUseCase, null
      );
      
      var result = await controller.RenewToken() as RedirectResult;
      
      Assert.AreEqual("/", result.Url);
    }

    [TestMethod]
    public async Task RenewTokenShould_GetAuthResponse(){
      var refreshToken = "refresh_token";
      cacheServiceMock = new Mock<ICacheService>();
      cacheServiceMock.Setup(cache => cache.GetValue<string>(KEYS.REFRESH_TOKEN))
                      .Returns(() => refreshToken);
      secureServiceMock = new Mock<ISecureService>();
      secureServiceMock.Setup(service => service.RenewToken(refreshToken))
                       .Returns(Task.FromResult(Result<AuthorizationResponse>.Ok(new AuthorizationResponse())));
      renewTokenUseCase = new RenewTokenUseCase(secureServiceMock.Object, cacheServiceMock.Object);
      renewTokenUseCase.RefreshToken = refreshToken;
      controller = new OAuth2Controller(
        null, null, renewTokenUseCase, null
      );
      
      var result = await controller.RenewToken() as OkObjectResult;
      
      Assert.IsInstanceOfType(result.Value, typeof(AuthorizationResponse));
    }
  }
}