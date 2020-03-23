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
        mockResponse = Mock.Of<HttpResponseMessage>();
        mockResponse.StatusCode = HttpStatusCode.OK;
    }
    private ServiceAgent agent { get; set; }
    private HttpResponseMessage mockResponse { get; set; }

    [TestMethod]
    public async Task FetchCounters_Should_Succeed(){
      var mockHandler = mockHttpMessageHandler(mockResponse);
      var client = new HttpClient(mockHandler.Object);
      agent = new ServiceAgent(client, TestDoubles.ServerUrls);
      var response = await agent.FetchCounters();
      Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [TestMethod]
    public async Task FetchToken_Should_Succeed(){
      var mockHandler = mockHttpMessageHandler(mockResponse);
      var client = new HttpClient(mockHandler.Object);
      agent = new ServiceAgent(client, TestDoubles.ServerUrls);
      var response = await agent.FetchToken(new AuthorizationRequest());
      Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [TestMethod]
    public async Task RenewToken_Should_Succeed(){
      var mockHandler = mockHttpMessageHandler(mockResponse);
      var client = new HttpClient(mockHandler.Object);
      agent = new ServiceAgent(client, TestDoubles.ServerUrls);
      var response = await agent.RenewToken(new AuthorizationRequest());
      Assert.IsTrue(response.IsSuccessStatusCode);
    }

    private Mock<HttpMessageHandler> mockHttpMessageHandler(HttpResponseMessage mockResponse){
      var handler = new Mock<HttpMessageHandler>();
      handler.Protected()
             .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
              )
             .ReturnsAsync(mockResponse);
      return handler;
    }
  }
}
