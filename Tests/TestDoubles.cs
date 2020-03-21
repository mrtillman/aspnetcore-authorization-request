using Moq;
using Common;
using Domain;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Tests {
  public static class TestDoubles {

    public static readonly string Token = "a2a3fa68-658a-4916-94ad-f1170a649839";
    public static readonly string ClientId = "10ae9344-e599-4c49-ab0c-2891521ccc46";
    public static readonly string ClientSecret = "013e6077-5ef1-4679-b353-a5792f9faabf";
    public static readonly string RefreshToken = "8681bade-6c3a-4b86-a38a-378da2bf1181";
    public static readonly string Code = "3bd5b7a0-876b-4a06-9aa8-903fe4d7e8e6";
    public static readonly string State = "54719797-8ee7-488c-824a-f40fd9fab348";
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

    private static IConfiguration _configuration = null;
    public static IConfiguration Configuration
    {
      get {
        if(_configuration == null){
          _configuration = Mock.Of<IConfiguration>(MockBehavior.Strict);
          Mock.Get(_configuration)
                  .Setup(config => config["CLIENT_ID"])
                  .Returns("CLIENT_ID");
          Mock.Get(_configuration)
                  .Setup(config => config["CLIENT_SECRET"])
                  .Returns("CLIENT_SECRET");
          Mock.Get(_configuration)
                  .Setup(config => config["REDIRECT_URI"])
                  .Returns("REDIRECT_URI");
          return _configuration;
        }
        return _configuration;
      }
    }
    public static readonly List<Counter> Counters = new List<Counter>();
  }
}
