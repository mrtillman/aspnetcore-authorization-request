using Moq;
using AuthDemo.Constants;

namespace AuthDemo.Tests.Mocks
{
  
  static partial class MockObjects
  {
    private static IServerUrls mockServerUrls { get; set; }

    public static IServerUrls ServerUrls
    {
      get {
        
        if(mockServerUrls != null) return mockServerUrls;

        mockServerUrls = Mock.Of<IServerUrls>();
        Mock.Get(mockServerUrls)
            .Setup(urls => urls.API)
            .Returns("https://api.counter-culture.io");
        return mockServerUrls;
      }
    }
  }
}
