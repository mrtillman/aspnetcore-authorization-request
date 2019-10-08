using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace AuthDemo.TestDoubles
{  
  static partial class Mock
  {
    public static Moq.Mock<HttpMessageHandler> HttpResponseMessage(HttpResponseMessage mockResponse)
      => TestDoubles.Mock.SetUp(handler => {
          handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                  )
                  .ReturnsAsync(mockResponse);
          return handler;
      });
  }
}
