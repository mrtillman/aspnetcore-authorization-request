using System;
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
  public class RenewTokenUseCaseTests
  {
    private Mock<ISecureService> secureServiceMock { get; set; }
    private Mock<ICacheService> cacheServiceMock { get; set; }

    [TestInitialize]
    public void Initialize()
    {
        secureServiceMock = new Mock<ISecureService>();
        cacheServiceMock = new Mock<ICacheService>();
    }

    [TestMethod]
    public async Task Execute_ShouldFail_IfRefreshTokenIsNull(){
      
      var useCase = new RenewTokenUseCase(secureServiceMock.Object, cacheServiceMock.Object);
      useCase.RefreshToken = null;

      var result = await useCase.Execute();

      Assert.IsTrue(result.DidFail);
      secureServiceMock.Verify(service => service.RenewToken(It.IsAny<string>()), Times.Never);
    }

    [TestMethod]
    public async Task Execute_Should_RenewToken(){
      var mockResult = Result<AuthorizationResponse>.Ok(new AuthorizationResponse());
      cacheServiceMock.Setup(cache => cache.SetValue(KEYS.REFRESH_TOKEN, It.IsAny<string>()))
                      .Verifiable();
      cacheServiceMock.Setup(cache => cache.GetValue<string>(KEYS.REFRESH_TOKEN))
                      .Returns(TestDoubles.RefreshToken);
      secureServiceMock.Setup(service => service.RenewToken(It.IsAny<string>()))
                       .Returns(Task.FromResult(mockResult))
                       .Verifiable();

      var useCase = new RenewTokenUseCase(secureServiceMock.Object, cacheServiceMock.Object);

      var result = await useCase.Execute();

      Assert.IsTrue(result.DidSucceed);
      secureServiceMock.Verify(service => service.RenewToken(It.IsAny<string>()), Times.Once);
    }
  }
}
