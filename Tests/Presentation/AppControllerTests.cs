using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using Services;
using Microsoft.AspNetCore.Http;
using Application;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;
using Common;

namespace Tests.Presentation {
  
  [TestClass]
  public class AppControllerTests
  {
    private AppController controller { get; set; }
    public RenewTokenUseCase renewTokenUseCase { get; set; }
    public GetTokenUseCase getTokenUseCase { get; set; }
    public GetCountersUseCase getCountersUseCase { get; set; }
    private Mock<ICacheService> cacheServiceMock { get; set; }
    private Mock<ISecureService> secureServiceMock { get; set; }
    private Mock<ICounterService> counterServiceMock { get; set; }

    [TestInitialize]
    public void Initialize() {
      cacheServiceMock = new Mock<ICacheService>();
      cacheServiceMock.Setup(cache => cache.Clear()).Verifiable();
      secureServiceMock = new Mock<ISecureService>();
      secureServiceMock.SetupGet(service => service.AuthorizationUrl)
                       .Returns(TestDoubles.AuthorizationUrl).Verifiable();
    }

    [TestMethod]
    public void Index_Should_Load(){
      controller = new AppController(null, null, null, cacheServiceMock.Object);

      var result = controller.Index();

      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void SignIn_Should_ClearCache(){
      getTokenUseCase = new GetTokenUseCase(secureServiceMock.Object, cacheServiceMock.Object);
      controller = new AppController(getTokenUseCase, null, null, null);
      var ctrlContext = new ControllerContext();
      var httpContext = new Mock<HttpContext>();
      httpContext.Setup(ctx => ctx.Response.Redirect(TestDoubles.AuthorizationUrl)).Verifiable();
      ctrlContext.HttpContext = httpContext.Object;
      controller.ControllerContext = ctrlContext;
      
      controller.SignIn();

      cacheServiceMock.Verify(cache => cache.Clear(), Times.Once);
    }

    [TestMethod]
    public void SignInShould_RedirectToAuthUrl(){
    getTokenUseCase = new GetTokenUseCase(secureServiceMock.Object, cacheServiceMock.Object);
      controller = new AppController(getTokenUseCase, null, null, null);
      var ctrlContext = new ControllerContext();
      var httpContext = new Mock<HttpContext>();
      httpContext.Setup(ctx => ctx.Response.Redirect(TestDoubles.AuthorizationUrl)).Verifiable();
      ctrlContext.HttpContext = httpContext.Object;
      controller.ControllerContext = ctrlContext;
      
      controller.SignIn();

      httpContext.Verify(ctx => ctx.Response.Redirect(TestDoubles.AuthorizationUrl), Times.Once);
    }

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
                       .Returns(Task.FromResult(Result.Ok(authResponse)));
      getTokenUseCase = new GetTokenUseCase(secureServiceMock.Object, cacheServiceMock.Object);
      counterServiceMock.Setup(service => service.GetCounters())
                        .Returns(Task.FromResult(Result.Ok(new List<Counter>())));
      getCountersUseCase = new GetCountersUseCase(counterServiceMock.Object);
      renewTokenUseCase = new RenewTokenUseCase(secureServiceMock.Object, cacheServiceMock.Object);
      
      controller = new AppController(
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
      controller = new AppController(
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
                       .Returns(Task.FromResult(Result.Ok(new AuthorizationResponse())));
      renewTokenUseCase = new RenewTokenUseCase(secureServiceMock.Object, cacheServiceMock.Object);
      renewTokenUseCase.RefreshToken = refreshToken;
      controller = new AppController(
        null, null, renewTokenUseCase, null
      );
      
      var result = await controller.RenewToken() as OkObjectResult;
      
      Assert.IsInstanceOfType(result.Value, typeof(AuthorizationResponse));
    }
  }
}
