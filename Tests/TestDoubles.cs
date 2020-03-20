using Moq;
using Common;
using Microsoft.Extensions.Configuration;

namespace Tests {
  public static class TestDoubles {

    private static IServerUrls _serverUrls = null;
    public static IServerUrls ServerUrls { 
      get {
        if(_serverUrls == null){
          _serverUrls = Mock.Of<IServerUrls>(MockBehavior.Strict);
          Mock.Get(_serverUrls)
              .Setup(urls => urls.API)
              .Returns("https://api.counter-culture.io");
          Mock.Get(_serverUrls)
              .Setup(urls => urls.SECURE)
              .Returns("https://secure.counter-culture.io");
          return _serverUrls;
        }
        return _serverUrls;
      }
    }

    public static IConfiguration Configuration
    {
      get {
        var configuration = Mock.Of<IConfiguration>(MockBehavior.Strict);
        Mock.Get(configuration)
                .Setup(config => config["CLIENT_ID"])
                .Returns("CLIENT_ID");
        Mock.Get(configuration)
                .Setup(config => config["CLIENT_SECRET"])
                .Returns("CLIENT_SECRET");
        Mock.Get(configuration)
                .Setup(config => config["REDIRECT_URI"])
                .Returns("REDIRECT_URI");
        return configuration;
      }
    }
  }
}
