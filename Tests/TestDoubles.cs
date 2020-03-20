using Moq;
using Common;

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
  }
}
