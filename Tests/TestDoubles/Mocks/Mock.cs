using System;
using System.Net.Http;

namespace Tests.TestDoubles
{
  static partial class Mock
  {
    public static HttpResponseMessage SetUp(Func<HttpResponseMessage, HttpResponseMessage> setup)
    {
      return setup(Moq.Mock.Of<HttpResponseMessage>());
    }

    public static Moq.Mock<HttpMessageHandler> SetUp(Func<Moq.Mock<HttpMessageHandler>, Moq.Mock<HttpMessageHandler>> setup)
    {
      return setup(new Moq.Mock<HttpMessageHandler>());
    }
  }
}
