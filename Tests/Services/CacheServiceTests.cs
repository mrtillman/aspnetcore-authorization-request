using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Common;
using Tests;

namespace Tests.Services
{
  [TestClass]
  public class CacheServiceTests
  {
    [TestInitialize]
    public void Initialize()
    {
      cacheService = new CacheService();
    }
    private ICacheService cacheService { get; set; }

    [TestMethod]
    public void Should_CacheAccessToken(){
      cacheService.SetValue(KEYS.ACCESS_TOKEN, TestDoubles.Token);

      var result = cacheService.GetValue<string>(KEYS.ACCESS_TOKEN);

      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void Should_CacheRefreshToken(){
      cacheService.SetValue(KEYS.REFRESH_TOKEN, TestDoubles.RefreshToken);

      var result = cacheService.GetValue<string>(KEYS.REFRESH_TOKEN);

      Assert.IsNotNull(result);
    }
  }
}
