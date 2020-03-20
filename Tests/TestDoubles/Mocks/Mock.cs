using System;
using System.Net.Http;

namespace Tests
{
  static partial class Mock
  {
    public static HttpResponseMessage SetUp(Func<HttpResponseMessage, HttpResponseMessage> setup)
    {
      return setup(Moq.Mock.Of<HttpResponseMessage>(Moq.MockBehavior.Strict));
    }

    public static Moq.Mock<HttpMessageHandler> SetUp(Func<Moq.Mock<HttpMessageHandler>, Moq.Mock<HttpMessageHandler>> setup)
    {
      return setup(new Moq.Mock<HttpMessageHandler>());
    }
  }
}
