using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Common;

namespace Tests.Services
{
  [TestClass]
  public class CacheServiceTests
  {
    [TestInitialize]
    public void TestStartup()
    {
      cacheService = new CacheService();
    }
    private ICacheService cacheService { get; set; }

    [TestMethod]
    public void Should_Cache_Access_Token(){
      cacheService.SetValue(KEYS.ACCESS_TOKEN, "access_token");

      var result = cacheService.GetValue<string>(KEYS.ACCESS_TOKEN);

      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void Should_Cache_Refresh_Token(){
      cacheService.SetValue(KEYS.REFRESH_TOKEN, "refresh_token");

      var result = cacheService.GetValue<string>(KEYS.REFRESH_TOKEN);

      Assert.IsNotNull(result);
    }
  }
}
