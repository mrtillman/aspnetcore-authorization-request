using Microsoft.Extensions.Configuration;

namespace Tests
{
  static partial class Mock
  {
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
