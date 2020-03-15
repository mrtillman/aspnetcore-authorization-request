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
    public void TestSetup()
    {
        secureServiceMock = new Mock<ISecureService>();
        cacheServiceMock = new Mock<ICacheService>();
    }

    [TestMethod]
    public async Task Should_Fail_If_RefreshToken_Is_Null(){
      
      var useCase = new RenewTokenUseCase(secureServiceMock.Object, cacheServiceMock.Object);
      useCase.RefreshToken = null;

      var result = await useCase.Execute();

      Assert.IsTrue(result.DidFail);
    }

    [TestMethod]
    public async Task Should_Get_AuthResponse(){
      var mockRefreshToken = Guid.NewGuid().ToString();
      var mockResult = Result<AuthorizationResponse>.Ok(new AuthorizationResponse());
      cacheServiceMock.Setup(cache => cache.SetValue(KEYS.REFRESH_TOKEN, It.IsAny<string>()))
                      .Verifiable();
      cacheServiceMock.Setup(cache => cache.GetValue<string>(KEYS.REFRESH_TOKEN))
                      .Returns(mockRefreshToken);
      secureServiceMock.Setup(service => service.RenewToken(It.IsAny<string>()))
                       .Returns(Task.FromResult(mockResult));      
      var useCase = new RenewTokenUseCase(secureServiceMock.Object, cacheServiceMock.Object);

      var result = await useCase.Execute();

      Assert.IsTrue(result.DidSucceed);
    }
  }
}
