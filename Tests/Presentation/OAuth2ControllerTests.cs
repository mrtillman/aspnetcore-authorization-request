using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation;
using Application;
using Moq;
using Domain;
using Common;
using System.Threading.Tasks;
using Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Presentation {
  
  [TestClass]
  public class OAuth2ControllerTests
  {
    
    public OAuth2Controller controller { get; set; }
    public RenewTokenUseCase renewTokenUseCase { get; set; }
    public GetTokenUseCase getTokenUseCase { get; set; }
    public GetCountersUseCase getCountersUseCase { get; set; }
    private Mock<ICacheService> cacheServiceMock { get; set; }
    private Mock<ISecureService> secureServiceMock { get; set; }
    private Mock<ICounterService> counterServiceMock { get; set; }

    [TestMethod]
    public async Task CallbackShould_GetCounters(){
      var authResponse = new AuthorizationResponse(){ 
        access_token = "access_token",
        refresh_token = "refresh_token"
      };
      cacheServiceMock = new Mock<ICacheService>();
      secureServiceMock = new Mock<ISecureService>();
      counterServiceMock = new Mock<ICounterService>();
      cacheServiceMock.Setup(cache => cache.GetValue<AuthorizationResponse>(KEYS.ACCESS_TOKEN))
                      .Returns(() => null);
      cacheServiceMock.Setup(cache => cache.SetValue(KEYS.ACCESS_TOKEN, It.IsAny<AuthorizationResponse>()))
                      .Verifiable();
      secureServiceMock.Setup(service => service.GetToken(It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(Task.FromResult(Result<AuthorizationResponse>.Ok(authResponse)));
      getTokenUseCase = new GetTokenUseCase(secureServiceMock.Object, cacheServiceMock.Object);
      counterServiceMock.Setup(service => service.GetCounters())
                        .Returns(Task.FromResult(Result<List<Counter>>.Ok(new List<Counter>())));
      getCountersUseCase = new GetCountersUseCase(counterServiceMock.Object);
      renewTokenUseCase = new RenewTokenUseCase(secureServiceMock.Object, cacheServiceMock.Object);
      
      controller = new OAuth2Controller(
        getTokenUseCase, getCountersUseCase, renewTokenUseCase, cacheServiceMock.Object
      );
      
      var result = await controller.Callback("code", "state");

      Assert.IsNotNull(result);
    }

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