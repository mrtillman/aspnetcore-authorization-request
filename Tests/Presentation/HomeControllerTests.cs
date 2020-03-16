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
    public void TestSetup() {
      cacheMock = new Mock<ICacheService>();
      cacheMock.Setup(cache => cache.Clear()).Verifiable();
      secureServiceMock = new Mock<ISecureService>();
      secureServiceMock.SetupGet(service => service.AuthorizationUrl)
                       .Returns("AuthUrl").Verifiable();
    }

    [TestMethod]
    public void Should_Load_HomePage(){
      controller = new HomeController(null, cacheMock.Object);

      var result = controller.Index();

      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void Should_ClearCache_OnIndex(){
      controller = new HomeController(null, cacheMock.Object);

      var result = controller.Index();

      cacheMock.Verify(cache => cache.Clear(), Times.Once);
    }

    [TestMethod]
    public void Should_RedirectToAuthUrl_OnSignIn(){
      controller = new HomeController(secureServiceMock.Object, cacheMock.Object);
      controller.ControllerContext = new ControllerContext();
      var httpContext = new Mock<HttpContext>();
      httpContext.Setup(ctx => ctx.Response.Redirect(It.IsAny<string>())).Verifiable();
      controller.ControllerContext.HttpContext = httpContext.Object;

      controller.SignIn();

      secureServiceMock.Verify(service => service.AuthorizationUrl, Times.Once);
      httpContext.Verify(ctx => ctx.Response.Redirect(It.IsAny<string>()), Times.Once);
    }
  }
}