using Moq;
using Microsoft.Extensions.Configuration;

namespace AuthDemo.Tests.Mocks  
{
  
  static partial class MockObjects
  {
    public static IConfiguration Configuration
    {
      get => Mock.Of<IConfiguration>();
    }
  }
}
