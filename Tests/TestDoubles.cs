using Moq;
using Common;
using Microsoft.Extensions.Configuration;

namespace Tests {
  public static class TestDoubles {

    private static IServerUrls _serverUrls = null;
    public static IServerUrls ServerUrls { 
      get {
        if(_serverUrls == null){
          _serverUrls = Moq.Mock.Of<IServerUrls>(Moq.MockBehavior.Strict);
          Moq.Mock.Get(_serverUrls)
              .Setup(urls => urls.API)
              .Returns("https://api.counter-culture.io");
          Moq.Mock.Get(_serverUrls)
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
        var configuration = Moq.Mock.Of<IConfiguration>(Moq.MockBehavior.Strict);
        Moq.Mock.Get(configuration)
                .Setup(config => config["CLIENT_ID"])
                .Returns("CLIENT_ID");
        Moq.Mock.Get(configuration)
                .Setup(config => config["CLIENT_SECRET"])
                .Returns("CLIENT_SECRET");
        Moq.Mock.Get(configuration)
                .Setup(config => config["REDIRECT_URI"])
                .Returns("REDIRECT_URI");
        return configuration;
      }
    }
  }
}
