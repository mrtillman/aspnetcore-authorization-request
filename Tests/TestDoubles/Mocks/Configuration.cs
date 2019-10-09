using Microsoft.Extensions.Configuration;

namespace AuthDemo.TestDoubles  
{
  static partial class Mock
  {
    public static IConfiguration Configuration
    {
      get {
        var configuration = Moq.Mock.Of<IConfiguration>();
        Moq.Mock.Get(configuration)
                .Setup(config => config["CLIENT_ID"])
                .Returns("CLIENT_ID");
        Moq.Mock.Get(configuration)
                .Setup(config => config["REDIRECT_URI"])
                .Returns("REDIRECT_URI");
        return configuration;
      }
    }
  }
}
