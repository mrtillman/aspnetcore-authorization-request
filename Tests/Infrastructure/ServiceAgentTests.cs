using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure;
using Domain;
using Moq;
using Moq.Protected;
using System.Threading;

namespace Tests.Infrastructure {
  
  [TestClass]
  public class ServiceAgentTests
  {
    public ServiceAgentTests()
    {
        mockResponse = Moq.Mock.Of<HttpResponseMessage>();
        mockResponse.StatusCode = HttpStatusCode.OK;
    }
    private ServiceAgent agent { get; set; }
    private HttpResponseMessage mockResponse { get; set; }

    [TestMethod]
    public async Task Should_FetchCounters(){
      var mockHandler = mockHttpMessageHandler(mockResponse);
      var client = new HttpClient(mockHandler.Object);
      agent = new ServiceAgent(client, TestDoubles.ServerUrls);
      var response = await agent.FetchCounters();
      Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [TestMethod]
    public async Task Should_FetchToken(){
      var mockHandler = mockHttpMessageHandler(mockResponse);
      var client = new HttpClient(mockHandler.Object);
      agent = new ServiceAgent(client, TestDoubles.ServerUrls);
      var response = await agent.FetchToken(new AuthorizationRequest());
      Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [TestMethod]
    public async Task Should_RenewToken(){
      var mockHandler = mockHttpMessageHandler(mockResponse);
      var client = new HttpClient(mockHandler.Object);
      agent = new ServiceAgent(client, TestDoubles.ServerUrls);
      var response = await agent.RenewToken(new AuthorizationRequest());
      Assert.IsTrue(response.IsSuccessStatusCode);
    }

    private Mock<HttpMessageHandler> mockHttpMessageHandler(HttpResponseMessage mockResponse)
      => Tests.Mock.SetUp(handler => {
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
