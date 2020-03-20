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

    public static string Counters
        => "[{\"_id\":\"5d16c0cd11ee4a3d6f44b045\",\"name\":\"alcohol\",\"value\":0,\"skip\":1,\"__v\":0},{\"_id\":\"5d16c0cd11ee4a3d6f44b046\",\"name\":\"tobacco\",\"value\":0,\"skip\":1,\"__v\":0},{\"_id\":\"5d16c0cd11ee4a3d6f44b047\",\"name\":\"firearms\",\"value\":0,\"skip\":1,\"__v\":0}]";

    public static string AuthorizationResponse
      => "{\"id_token\":\"id_token\", \"access_token\":\"access_token\", \"expires_in\":86400, \"token_type\":\"Bearer\",  \"scope\":\"openid\"}";

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
