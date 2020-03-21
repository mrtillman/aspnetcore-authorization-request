using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using Services;
using Microsoft.AspNetCore.Http;

namespace Tests.Presentation {
  
  [TestClass]
  public class HomeControllerTests
  {
    private HomeController controller { get; set; }

    private Mock<ICacheService> cacheMock { get; set; }
    private Mock<ISecureService> secureServiceMock { get; set; }

    [TestInitialize]
    public void Initialize() {
      cacheMock = new Mock<ICacheService>();
      cacheMock.Setup(cache => cache.Clear()).Verifiable();
      secureServiceMock = new Mock<ISecureService>();
      secureServiceMock.SetupGet(service => service.AuthorizationUrl)
                       .Returns("AuthUrl").Verifiable();
    }

    [TestMethod]
    public void IndexShould_Load(){
      controller = new HomeController(null, cacheMock.Object);

      var result = controller.Index();

      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void IndexShould_ClearCache(){
      controller = new HomeController(null, cacheMock.Object);

      var result = controller.Index();

      cacheMock.Verify(cache => cache.Clear(), Times.Once);
    }

    [TestMethod]
    public void SignInShould_RedirectToAuthUrl(){
      var authUrl = secureServiceMock.Object.AuthorizationUrl;
      controller = new HomeController(secureServiceMock.Object, cacheMock.Object);
      controller.ControllerContext = new ControllerContext();
      var httpContext = new Mock<HttpContext>();
      httpContext.Setup(ctx => ctx.Response.Redirect(authUrl)).Verifiable();
      controller.ControllerContext.HttpContext = httpContext.Object;

      controller.SignIn();

      httpContext.Verify(ctx => ctx.Response.Redirect(authUrl), Times.Once);
    }
  }
}