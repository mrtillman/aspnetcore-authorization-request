using Microsoft.Extensions.Configuration;

namespace AuthDemo.TestDoubles  
{
  static partial class Mock
  {
    public static IConfiguration Configuration
    {
      get => Moq.Mock.Of<IConfiguration>();
    }
  }
}
