using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure;
using Tests.TestDoubles;
using Domain;

namespace Tests.Infrastructure {
  
  [TestClass]
  public class ServiceAgentTests
  {
    public ServiceAgent agent { get; set; }

    private readonly HttpResponseMessage mockResponse = Mock.SetUp(res => {
      res.StatusCode = HttpStatusCode.OK;
      return res;
    });

    [TestMethod]
    public async Task Should_Fetch_Counters(){
      var mockHandler = Mock.HttpMessageHandler(mockResponse);
      var client = new HttpClient(mockHandler.Object);
      agent = new ServiceAgent(client, Mock.ServerUrls);
      var response = await agent.FetchCounters();
      Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [TestMethod]
    public async Task Should_Fetch_Token(){
      var mockHandler = Mock.HttpMessageHandler(mockResponse);
      var client = new HttpClient(mockHandler.Object);
      agent = new ServiceAgent(client, Mock.ServerUrls);
      var response = await agent.FetchToken(new AuthorizationRequest());
      Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [TestMethod]
    public async Task Should_Renew_Token(){
      var mockHandler = Mock.HttpMessageHandler(mockResponse);
      var client = new HttpClient(mockHandler.Object);
      agent = new ServiceAgent(client, Mock.ServerUrls);
      var response = await agent.RenewToken(new AuthorizationRequest());
      Assert.IsTrue(response.IsSuccessStatusCode);
    }
  }
}
